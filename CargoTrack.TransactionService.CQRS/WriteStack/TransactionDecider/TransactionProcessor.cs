using System;
using System.Collections.Generic;
using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.Common.Exceptions;
using CargoTrack.OrganizationService.Contracts;
using CargoTrack.OrganizationService.Contracts.Models.DTO;
using CargoTrack.OrganizationService.Contracts.Models.Service;
using CargoTrack.OrganizationService.Contracts.Models.Service.Organization;
using CargoTrack.TransactionService.CQRS.Common.Events;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;
using CargoTrack.TransactionService.CQRS.WriteStack.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        private readonly IOrganizationService _organizationService;

        public TransactionProcessor(IUnitOfWork unitOfWork, IEventBus eventBus, IOrganizationService organizationService)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
            _organizationService = organizationService;
        }

        /// <summary>
        /// Delivery to store 
        /// </summary>
        /// <remarks>
        /// -Amount Source Organization (Warehouse)
        ///  +Amount Target Organization (Store)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void DeliveryToStore(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {

            if (!command.SourceOrganizationId.HasValue | !command.TargetOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_OR_TARGET_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
                _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
                {
                    Id = command.SourceOrganizationId.Value
                });

            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!sourceOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("warehouse", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_SOURCE_ORGANIZATION_IS_NOT_WAREOUSE");

            var targetOrganizationResponse =
            _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
            {
                Id = command.TargetOrganizationId.Value
            });
            if (targetOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(targetOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!targetOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("store", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_TARGET_ORGANIZATION_IS_NOT_STORE");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = -command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });
            var targetLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = command.Amount,
                OrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });
            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo, sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetLedgerTransactionEvent(command, targetLedger, cargo, targetOrganizationResponse.OrganizationDetailed));

            // Minus balance for source
            var balanceSourse = AddOrUpdateMinusBalance(command.Amount,
                sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);
            // Plus balance for source
            var balanceTarget = AddOrUpdatePlusBalance(command.Amount,
                targetOrganizationResponse.OrganizationDetailed.Id, cargo.Id, targetLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger, targetLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TargetOrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo, sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceTarget, cargo, targetOrganizationResponse.OrganizationDetailed));


        }

        /// <summary>
        /// Receive cargo from supplier
        /// </summary>
        /// <remarks>
        /// -Amount Source Organization (Supplier)
        /// +Amount Target Organization (Warehouse)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void ReceiveCargoFromSupplier(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {
            if (!command.SourceOrganizationId.HasValue | !command.TargetOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_OR_TARGET_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
                _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
                {
                    Id = command.SourceOrganizationId.Value
                });
            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!sourceOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("supplier", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_SOURCE_ORGANIZATION_IS_NOT_SUPPLIER");

            var targetOrganizationResponse =
                _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
                {
                    Id = command.TargetOrganizationId.Value
                });
            if (targetOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(targetOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!targetOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("warehouse", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_TARGET_ORGANIZATION_IS_NOT_WAREHOUSE");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = -command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });
            var targetLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = command.Amount,
                OrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo,
                sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetLedgerTransactionEvent(command, targetLedger, cargo,
                targetOrganizationResponse.OrganizationDetailed));

            // Minus balance for source
            var balanceSourse = AddOrUpdateMinusBalance(command.Amount,
                sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);
            // Plus balance for target
            var balanceTarget = AddOrUpdatePlusBalance(command.Amount,
                targetOrganizationResponse.OrganizationDetailed.Id, cargo.Id, targetLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger, targetLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TargetOrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo,
                sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceTarget, cargo,
                targetOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        /// Status correction 
        /// </summary>
        /// <remarks>
        /// +Amount Source Organization (Any type - Negative amount allowed)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void StatusCorrection(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {
            if (!command.SourceOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
             _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
             {
                 Id = command.SourceOrganizationId.Value
             });
            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo, sourceOrganizationResponse.OrganizationDetailed));

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });

            var balanceSourse = AddOrUpdatePlusBalance(command.Amount,
                sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);

            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo, sourceOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        /// Buy cargo
        /// </summary>
        /// <remarks>
        /// +Amount Target Organization (Warehouse)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void BuyCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {

            if (!command.TargetOrganizationId.HasValue)
                throw new ArgumentException("NO_TARGET_ORGANIZATION_IN_REQUEST");

            var targetOrganizationResponse =
             _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
             {
                 Id = command.TargetOrganizationId.Value
             });
            if (targetOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(targetOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!targetOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("warehouse", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_TARGET_ORGANIZATION_IS_NOT_WAREHOUSE");

            var targetLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = command.Amount,
                OrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, targetLedger, cargo, targetOrganizationResponse.OrganizationDetailed));

            var balanceTarget = AddOrUpdatePlusBalance(command.Amount,
                targetOrganizationResponse.OrganizationDetailed.Id, cargo.Id, targetLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { targetLedger },
                TargetOrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });
            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceTarget, cargo, targetOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        /// Sell cargo 
        /// </summary>
        /// <remarks>
        /// -Amount Source Organization (Warehouse)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void SellCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {
            if (!command.SourceOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
             _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
             {
                 Id = command.SourceOrganizationId.Value
             });
            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");
            if (!sourceOrganizationResponse.OrganizationDetailed.OrganizationType.Equals("warehouse", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("TYPE_OF_TARGET_ORGANIZATION_IS_NOT_WAREHOUSE");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = -command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo, sourceOrganizationResponse.OrganizationDetailed));

            var balanceSourse = AddOrUpdateMinusBalance(command.Amount,
                sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });
            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo, sourceOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        ///  Manual transfer of cargo  
        /// </summary>
        /// <remarks>
        ///  +Amount Source Organization (Any type - Negative amount allowed)
        ///  -Amount Target Organization (Any type - Negative amount allowed)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void ManualTransferOfCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {
            if (!command.SourceOrganizationId.HasValue | !command.TargetOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_OR_TARGET_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
                _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
                {
                    Id = command.SourceOrganizationId.Value
                });
            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");

            var targetOrganizationResponse =
                _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
                {
                    Id = command.TargetOrganizationId.Value
                });
            if (targetOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(targetOrganizationResponse.ErrorMessage, "OrganizationService");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            var targetLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = -command.Amount,
                OrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo, sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetLedgerTransactionEvent(command, targetLedger, cargo, targetOrganizationResponse.OrganizationDetailed));

            var balanceSourse = AddOrUpdatePlusBalance(command.Amount, sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);
            var balanceTarget = AddOrUpdateMinusBalance(command.Amount,
                targetOrganizationResponse.OrganizationDetailed.Id, cargo.Id, targetLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger, targetLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TargetOrganizationId = targetOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo, sourceOrganizationResponse.OrganizationDetailed));
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceTarget, cargo, targetOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        /// Trash cargo    
        /// </summary>
        /// <remarks>
        /// -Amount Source Organization (Any type)
        /// </remarks>
        /// <param name="command"></param>
        /// <param name="cargo"></param>
        public void TrashCargo(AddTransactionCommand command, CargoTypeDetailedDTO cargo)
        {
            if (!command.SourceOrganizationId.HasValue)
                throw new ArgumentException("NO_SOURCE_ORGANIZATION_IN_REQUEST");

            var sourceOrganizationResponse =
             _organizationService.GetOrganizationById(new GetOrganizationByIdRequest
             {
                 Id = command.SourceOrganizationId.Value
             });
            if (sourceOrganizationResponse.ServiceStatus != ServiceStatus.Success)
                throw new ServiceErrorException(sourceOrganizationResponse.ErrorMessage, "OrganizationService");

            var sourceLedger = _unitOfWork.LedgerTransactionRepository.Add(new LedgerTransaction()
            {
                Amount = -command.Amount,
                OrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                TotalPrice = command.Amount * cargo.Price
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetLedgerTransactionEvent(command, sourceLedger, cargo, sourceOrganizationResponse.OrganizationDetailed));


            var balanceSourse = AddOrUpdateMinusBalance(command.Amount,
                sourceOrganizationResponse.OrganizationDetailed.Id, cargo.Id, sourceLedger.Id);

            // Add transaction
            _unitOfWork.TransactionRepository.Add(new Transaction()
            {
                Amount = command.Amount,
                CargoId = cargo.Id,
                Comments = command.Comments,
                LedgerTransactions = new List<LedgerTransaction>() { sourceLedger },
                SourceOrganizationId = sourceOrganizationResponse.OrganizationDetailed.Id,
                Timestamp = DateTime.Now,
                TransactionType = command.TransactionType,
                UserId = "test"
            });

            _unitOfWork.Commit();
            _eventBus.Publish(GetBalanceUpdatedEvent(command, balanceSourse, cargo, sourceOrganizationResponse.OrganizationDetailed));
        }

        /// <summary>
        /// Get leget transaction event
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ledgerTransaction"></param>
        /// <param name="cargo"></param>
        /// <param name="organizationDetailedDto"></param>
        /// <returns></returns>
        private LedgerTransactionAddedEvent GetLedgerTransactionEvent(AddTransactionCommand cmd,
            LedgerTransaction ledgerTransaction, CargoTypeDetailedDTO cargo, OrganizationDetailedDTO organizationDetailedDto)
        {
            return new LedgerTransactionAddedEvent
            {
                Id = ledgerTransaction.Id,
                Amount = ledgerTransaction.Amount,
                OrganizationId = ledgerTransaction.OrganizationId,
                TotalPrice = ledgerTransaction.TotalPrice,
                TransactionType = cmd.TransactionType,
                CargoId = cmd.CargoId,
                Comments = cmd.Comments,
                Timestamp = DateTime.Now,
                Ean = cargo.Ean,
                Kardex = organizationDetailedDto.Kardex
            };
        }

        /// <summary>
        /// Get balance updated event
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="balance"></param>
        /// <param name="cargo"></param>
        /// <param name="organizationDetailedDto"></param>
        /// <returns></returns>
        private BalanceUpdatedEvent GetBalanceUpdatedEvent(AddTransactionCommand cmd, Balance balance,
            CargoTypeDetailedDTO cargo, OrganizationDetailedDTO organizationDetailedDto)
        {
            return new BalanceUpdatedEvent()
            {

                Id = balance.Id,
                Amount = balance.CargoBalance,
                OrganizationId = balance.OrganizationId,
                Description = cmd.Comments,
                Ean = cargo.Ean,
                CargoId = cmd.CargoId,
                Kardex = organizationDetailedDto.Kardex

            };
        }

        /// <summary>
        /// Add or update negative balance
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="organizationId"></param>
        /// <param name="cargoId"></param>
        /// <param name="lastLedgerTransaction"></param>
        /// <returns></returns>
        private Balance AddOrUpdateMinusBalance(int amount, int organizationId, int cargoId,
            int lastLedgerTransaction)
        {
            var balance =
           _unitOfWork.BalanceRepository.GetBalanceByOrganizationAndCargoId(organizationId, cargoId);
            if (balance == null)
            {
                var newBalance = new Balance()
                {
                    CargoBalance = -amount,
                    CargoId = cargoId,
                    OrganizationId = organizationId,
                    LastUpdated = DateTime.Now,
                    LastUpdatedLedgerTransactionId = lastLedgerTransaction
                };
                _unitOfWork.BalanceRepository.Add(newBalance);
                return newBalance;
            }
            balance.CargoBalance -= amount;
            balance.LastUpdated = DateTime.Now;
            balance.LastUpdatedLedgerTransactionId = lastLedgerTransaction;

            _unitOfWork.BalanceRepository.Update(balance);
            return balance;
        }

        /// <summary>
        /// Add or update positive balance
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="organizationId"></param>
        /// <param name="cargoId"></param>
        /// <param name="lastLedgerTransaction"></param>
        /// <returns></returns>
        private Balance AddOrUpdatePlusBalance(int amount, int organizationId, int cargoId,
           int lastLedgerTransaction)
        {
            var balance =
           _unitOfWork.BalanceRepository.GetBalanceByOrganizationAndCargoId(organizationId, cargoId);
            if (balance == null)
            {
                var newBalance = new Balance
                {
                    CargoBalance = +amount,
                    CargoId = cargoId,
                    OrganizationId = organizationId,
                    LastUpdated = DateTime.Now,
                    LastUpdatedLedgerTransactionId = lastLedgerTransaction
                };
                _unitOfWork.BalanceRepository.Add(newBalance);
                return newBalance;
            }
            balance.CargoBalance += amount;
            balance.LastUpdated = DateTime.Now;
            balance.LastUpdatedLedgerTransactionId = lastLedgerTransaction;

            _unitOfWork.BalanceRepository.Update(balance);
            return balance;
        }
    }
}

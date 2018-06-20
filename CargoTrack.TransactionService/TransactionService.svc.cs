using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using CargoTrack.Common.Exceptions;
using CargoTrack.TransactionService.Contracts;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.Contracts.Models.Service;
using CargoTrack.TransactionService.Contracts.Models.Service.Balance;
using CargoTrack.TransactionService.Contracts.Models.Service.Commands;
using CargoTrack.TransactionService.Contracts.Models.Service.Transaction;
using CargoTrack.TransactionService.CQRS.Common.Commands;
using CargoTrack.TransactionService.Infrastructure;
using log4net;
using CargoTrack.TransactionService.CQRS.Common.QueryFacades;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;

namespace CargoTrack.TransactionService
{
    [AutomapServiceBehavior]
    public class TransactionService : ITransactionService
    {
        #region Fields

        private readonly ILog _log = LogManager.GetLogger(typeof(TransactionService));
        public readonly IQueryFacade _queryFacade;
        public readonly ICommandBus _commandBus;

        #endregion

        #region Constructors

        public TransactionService(IQueryFacade queryFacade, ICommandBus commandBus)
        {
            _queryFacade = queryFacade;
            _commandBus = commandBus;
        }

        #endregion

        /// <summary>
        /// Get organization balanced for a selected organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetOrganizationBalancesByIdResponse GetOrganizationBalancesByOrganizationId(GetOrganizationBalancesByIdRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var response = _queryFacade.GetBalanceByOrganizationId(request.OrganizationId);

                if (response == null)
                    return new GetOrganizationBalancesByIdResponse(null, ServiceStatus.ServiceError, "Unable to lookup Organization Balance");

                return new GetOrganizationBalancesByIdResponse(Mapper.Map<ICollection<BalanceDTO>>(response), ServiceStatus.Success, string.Empty);
            }
            catch (ArgumentException ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationBalancesByIdResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationBalancesByIdResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Get organization balances by Kardex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetOrganizationBalancesByKardexResponse GetOrganizationBalancesByKardex(GetOrganizationBalancesByKardexRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            try
            {
                var response = _queryFacade.GetBalanceByKardex(request.Kardex);

                if (response == null)
                    return new GetOrganizationBalancesByKardexResponse(null, ServiceStatus.ServiceError, "Unable to lookup Organization Balance");

                return new GetOrganizationBalancesByKardexResponse(Mapper.Map<ICollection<BalanceDTO>>(response), ServiceStatus.Success, string.Empty);
            }
            catch (ArgumentException ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationBalancesByKardexResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationBalancesByKardexResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Get ledger transactions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetLedgerTransactionsResponse GetLedgerTransactions(GetLedgerTransactionsRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var response = _queryFacade.GetLedgerTransactions(request.OrganizationId, request.CargoId, request.Skip, request.Take);

                if (response == null)
                    return new GetLedgerTransactionsResponse(null, ServiceStatus.ServiceError, "Unable to lookup Organization Balance");

                return
                    new GetLedgerTransactionsResponse(Mapper.Map<ICollection<LedgerTransactionDTO>>(response), ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetLedgerTransactionsResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Get details about a ledger transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetLedgerTransactionDetailsResponse GetLedgerTransactionDetails(GetLedgerTransactionDetailsRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var response = _queryFacade.GetLedgerTransactionDetails(request.LedgerTransactionId);

                if (response == null)
                    return new GetLedgerTransactionDetailsResponse(null, ServiceStatus.ServiceError, "Unable to lookup Organization Balance");

                return
                    new GetLedgerTransactionDetailsResponse(Mapper.Map<LedgerTransactionDetailedDTO>(response), ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetLedgerTransactionDetailsResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Add a transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddTransactionCommandResponse AddTransaction(AddTransactionCommandRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                if (request.AddTransactionCommand == null)
                    throw new ServiceErrorException(nameof(request.AddTransactionCommand), nameof(TransactionService));

                _commandBus.Send(Mapper.Map<AddTransactionCommand>(request.AddTransactionCommand));

                return new AddTransactionCommandResponse(ServiceStatus.Success, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(request, ex);
                return new AddTransactionCommandResponse(ServiceStatus.ServiceError, ex.Message);
            }
            catch (ArgumentException ex)
            {
                _log.Error(request, ex);
                return new AddTransactionCommandResponse(ServiceStatus.ServiceError, ex.Message);
            }
            catch (ServiceErrorException ex)
            {
                _log.Error(request, ex);
                return new AddTransactionCommandResponse(ServiceStatus.ServiceError, ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new AddTransactionCommandResponse(ServiceStatus.ServiceError, string.Empty);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }
    }
}

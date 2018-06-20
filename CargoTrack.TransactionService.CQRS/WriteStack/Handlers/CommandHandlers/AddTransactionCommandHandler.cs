using System;
using System.Collections.Generic;
using System.Linq;
using CargoTrack.CargoService.Contracts;
using CargoTrack.CargoService.Contracts.Models.Service.Cargo;
using CargoTrack.Common.Exceptions;
using CargoTrack.TransactionService.CQRS.Common.Commands;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;
using CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Handlers.CommandHandlers
{
    /// <summary>
    /// Handler for add transaction commands
    /// </summary>
    public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand>
    {

        private readonly ICargoService _cargoService;
        private readonly ITransactionEngine _transactionEngine;

        public AddTransactionCommandHandler(ICargoService cargoService, ITransactionEngine transactionEngine)
        {
            _cargoService = cargoService;
            _transactionEngine = transactionEngine;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command"></param>
        public void Execute(AddTransactionCommand command)
        {
            var getCargoResponse =
                 _cargoService.GetCargoById(new GetCargoByIdRequest { Id = command.CargoId });

            if (getCargoResponse.ServiceStatus != CargoService.Contracts.Models.Service.ServiceStatus.Success)
                throw new ServiceErrorException(getCargoResponse.ErrorMessage, "CargoService");

            if (command.Amount < 0 && !AllowedNegativeTransactionTypes().Contains(command.TransactionType))
                throw new ArgumentException("AMOUNT_IS_NEGATIVE");

            int lastUpdatedLedgerTransactionId = _transactionEngine.ProccessTransaction(command, getCargoResponse.CargoTypeDetailed);
            if (lastUpdatedLedgerTransactionId == -1)
                throw new ServiceErrorException("TRANSACTION_TYPE_NOT_FOUND", "TransactionService");

        }

        /// <summary>
        /// The transaction types that allows negative transactions
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> AllowedNegativeTransactionTypes()
        {
            return new List<int>()
            {
                3,
                6
            };
        }
    }
}

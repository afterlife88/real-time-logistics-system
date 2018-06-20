using System;
using System.Collections.Generic;
using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.WriteStack.Commands;

namespace CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider
{
    /// <summary>
    /// Transaction engine for handling new transactions. Responsible for generating new ledger transactions.
    /// </summary>
    public class TransactionEngine : ITransactionEngine
    {
        private readonly Dictionary<int, Action<AddTransactionCommand, CargoTypeDetailedDTO>> _transactionHandlers;

        private readonly ITransactionProcessor _transactionProcessor;

        public TransactionEngine(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
            _transactionHandlers = CreateDictionary();
        }

        private Dictionary<int, Action<AddTransactionCommand, CargoTypeDetailedDTO>> CreateDictionary()
        {
            var dictionary = new Dictionary<int, Action<AddTransactionCommand, CargoTypeDetailedDTO>>
                                 {
                                     {1, _transactionProcessor.DeliveryToStore},
                                     {2, _transactionProcessor.ReceiveCargoFromSupplier },
                                     {3, _transactionProcessor.StatusCorrection},
                                     {4, _transactionProcessor.BuyCargo },
                                     {5,  _transactionProcessor.SellCargo},
                                     {6,  _transactionProcessor.ManualTransferOfCargo},
                                     {7,  _transactionProcessor.TrashCargo},
                                 };
            return dictionary;
        }

        /// <summary>
        /// Processes an add transaction command
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="cargoDTO"></param>
        /// <returns></returns>
        public int ProccessTransaction(AddTransactionCommand cmd, CargoTypeDetailedDTO cargoDTO)
        {
            if (_transactionHandlers.ContainsKey(cmd.TransactionType))
            {
                _transactionHandlers[cmd.TransactionType](cmd, cargoDTO);
                return 0;
            }
            return -1;

        }
    }
}

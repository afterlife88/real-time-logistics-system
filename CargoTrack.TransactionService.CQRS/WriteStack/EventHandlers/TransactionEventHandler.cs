using System;
using CargoTrack.TransactionService.CQRS.Common.Events;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;
using CargoTrack.TransactionService.CQRS.WriteStack.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.EventHandlers
{
    public class TransactionEventHandler : IEventHandler<TransactionAddedEvent>
    {
        private IDataStore<Balance> _balanceDataStore;
        private IDataStore<LedgerTransactionDetailed> _ledgerTransactionDetailedDataStore;
        private IDataStore<LedgerTransaction> _ledgerTransactionDataStore;
        public TransactionEventHandler(IDataStore<Balance> balanceDataStore,
            IDataStore<LedgerTransactionDetailed> ledgerTransactionDetailedDataStore,
            IDataStore<LedgerTransaction> ledgerTransactionDataStore)
        {
            _balanceDataStore = balanceDataStore;
            _ledgerTransactionDetailedDataStore = ledgerTransactionDetailedDataStore;
            _ledgerTransactionDataStore = ledgerTransactionDataStore;
        }
        public void HandleEvent(TransactionAddedEvent @event)
        {

            // todo: place all stuff to denormalize db's
            //_balanceDataStore.Add();
            throw new NotImplementedException();
        }
    }
}

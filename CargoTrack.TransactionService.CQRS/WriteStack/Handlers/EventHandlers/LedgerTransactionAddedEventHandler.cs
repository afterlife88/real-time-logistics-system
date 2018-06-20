using CargoTrack.TransactionService.CQRS.Common.Events;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;
using CargoTrack.TransactionService.CQRS.WriteStack.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Handlers.EventHandlers
{
    /// <summary>
    /// Handler for ledger transaction added event
    /// </summary>
    public class LedgerTransactionAddedEventHandler : IEventHandler<LedgerTransactionAddedEvent>
    {
        private readonly ILedgerTransactionDetailedDataStore _ledgerTransactionDetailedDataStore;
        private readonly ILedgerTransactionsDataStore _ledgerTransactionDataStore;

        public LedgerTransactionAddedEventHandler(ILedgerTransactionDetailedDataStore ledgerTransactionDetailedDataStore,
            ILedgerTransactionsDataStore ledgerTransactionDataStore)
        {
            _ledgerTransactionDetailedDataStore = ledgerTransactionDetailedDataStore;
            _ledgerTransactionDataStore = ledgerTransactionDataStore;
        }

        /// <summary>
        /// Handle the event
        /// </summary>
        /// <param name="event"></param>
        public void HandleEvent(LedgerTransactionAddedEvent @event)
        {
            _ledgerTransactionDataStore.Add(new LedgerTransaction()
            {
                Id = @event.Id,
                Amount = @event.Amount,
                OrganizationId = @event.OrganizationId,
                CargoId = @event.CargoId,
                Timestamp = @event.Timestamp,
                TransactionType = @event.TransactionType,
                TotalPrice = @event.TotalPrice
            });

            _ledgerTransactionDetailedDataStore.Add(new LedgerTransactionDetailed()
            {
                Id = @event.Id,
                Amount = @event.Amount,
                OrganizationId = @event.OrganizationId,
                CargoId = @event.CargoId,
                Timestamp = @event.Timestamp,
                TransactionType = @event.TransactionType,
                Comments = @event.Comments,
                TotalPrice = @event.TotalPrice,
                UserId = @event.UserId,
                UserName = @event.UserName,
                Ean = @event.Ean,
                Kardex = @event.Kardex
            });
        }
    }
}

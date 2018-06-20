using CargoTrack.TransactionService.CQRS.Common.Events;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;
using CargoTrack.TransactionService.CQRS.WriteStack.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Handlers.EventHandlers
{
    /// <summary>
    /// Handler for balance updated events
    /// </summary>
    public class BalanceUpdatedEventHandler : IEventHandler<BalanceUpdatedEvent>
    {
        private readonly IBalanceDataStore _balanceDataStore;

        public BalanceUpdatedEventHandler(IBalanceDataStore balanceDataStore)
        {
            _balanceDataStore = balanceDataStore;
        }
        
        /// <summary>
        /// Handle the event
        /// </summary>
        /// <param name="event"></param>
        public void HandleEvent(BalanceUpdatedEvent @event)
        {
            var existedBalance = _balanceDataStore.GetById(@event.Id);
            if (existedBalance != null)
                _balanceDataStore.Delete(existedBalance);

            _balanceDataStore.Add(new Balance()
            {
                Id = @event.Id,
                Amount = @event.Amount,
                OrganizationId = @event.OrganizationId,
                Ean = @event.Ean,
                Kardex = @event.Kardex,
                Description = $"Balance for cargo: {@event.Description}",
                CargoId = @event.CargoId
            });
        }
    }
}

namespace CargoTrack.TransactionService.CQRS.Common.Events
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
    }
}

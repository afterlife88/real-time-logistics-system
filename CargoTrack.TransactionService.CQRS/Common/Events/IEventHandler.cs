namespace CargoTrack.TransactionService.CQRS.Common.Events
{
    public interface IEventHandler<in TEvent>
         where TEvent : Event
    {
        //Action<Event> HandleEvent(TEvent @event);
        void HandleEvent(TEvent @event);
    }
}

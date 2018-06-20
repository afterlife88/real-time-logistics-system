using System.Collections.Generic;
using Ninject;

namespace CargoTrack.TransactionService.CQRS.Common.Events
{
    public class EventBus : IEventBus
    {
        private readonly IKernel _container;

        public EventBus(IKernel container)
        {
            _container = container;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var handler = _container.Get<IEventHandler<TEvent>>();

            handler.HandleEvent(@event);
        }
        public void Publish<TEvent>(IEnumerable<TEvent> messages)
          where TEvent : Event
        {
            foreach (var message in messages)
                Publish(message);
        }
    }
}


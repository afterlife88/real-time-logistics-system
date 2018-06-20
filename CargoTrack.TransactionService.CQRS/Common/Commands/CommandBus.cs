using Ninject;

namespace CargoTrack.TransactionService.CQRS.Common.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IKernel _container;

        public CommandBus(IKernel container)
        {
            _container = container;
        }

        public void Send<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = _container.Get<ICommandHandler<TCommand>>();
            handler.Execute(command);
        }
    }
}

namespace CargoTrack.TransactionService.CQRS.Common.Commands
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command) where TCommand : Command;
    }
}

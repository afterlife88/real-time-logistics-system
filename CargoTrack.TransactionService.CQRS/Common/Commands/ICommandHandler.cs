namespace CargoTrack.TransactionService.CQRS.Common.Commands
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand command);
    }
}

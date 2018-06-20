namespace CargoTrack.TransactionService.CQRS.Common.Queries
{
    public interface IQueryDispatcher
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}

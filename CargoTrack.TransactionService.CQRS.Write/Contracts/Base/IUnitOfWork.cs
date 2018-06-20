namespace CargoTrack.TransactionService.CQRS.Write.Contracts.Base
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes in database
        /// </summary>
        /// <returns></returns>
        void Commit();
        IBalanceRepository BalanceRepository { get; }
        ILedgerTransactionRepository LedgerTransactionRepository { get; }
        ITransactionRepository TransactionRepository { get; }
    }
}

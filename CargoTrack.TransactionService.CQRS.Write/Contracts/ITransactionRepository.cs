using System.Linq;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.Write.Contracts
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IQueryable<Transaction> GetAllTransactionsWithLedger();
    }
}

using System.Data.Entity;
using System.Linq;
using CargoTrack.TransactionService.CQRS.Write.Contracts;
using CargoTrack.TransactionService.CQRS.Write.Entities;
using CargoTrack.TransactionService.CQRS.Write.Repositories.Base;

namespace CargoTrack.TransactionService.CQRS.Write.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }

        public IQueryable<Transaction> GetAllTransactionsWithLedger()
        {
            return GetAll().Include(r => r.LedgerTransactions);
        }
    }
}

using CargoTrack.TransactionService.CQRS.Write.Contracts;
using CargoTrack.TransactionService.CQRS.Write.Entities;
using CargoTrack.TransactionService.CQRS.Write.Repositories.Base;

namespace CargoTrack.TransactionService.CQRS.Write.Repositories
{
    public class LedgerTransactionRepository : Repository<LedgerTransaction>, ILedgerTransactionRepository
    {
        public LedgerTransactionRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }

        public new LedgerTransaction Add(LedgerTransaction entity)
        {
            var item = this.DbContext.Set<LedgerTransaction>().Add(entity);
            return item;
        }
    }
}

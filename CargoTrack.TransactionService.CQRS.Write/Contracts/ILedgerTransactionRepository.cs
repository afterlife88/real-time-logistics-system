using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.Write.Contracts
{
    public interface ILedgerTransactionRepository : IRepository<LedgerTransaction>
    {
        new LedgerTransaction Add(LedgerTransaction entity);


    }
}

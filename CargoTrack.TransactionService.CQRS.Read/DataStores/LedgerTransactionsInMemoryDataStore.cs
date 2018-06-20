using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.DataStores
{
    public class LedgerTransactionsInMemoryDataStore : DataStore<LedgerTransaction>, ILedgerTransactionsDataStore
    {
        public ICollection<LedgerTransaction> GetLedgerTransactions(int organizationId, int cargoId, int skip, int take)
        {
            return
                GetAll()
                    .Where(lt => lt.OrganizationId == organizationId && lt.CargoId == cargoId)
                    .Skip(skip)
                    .Take(take)
                    .ToList();
        }
    }
}

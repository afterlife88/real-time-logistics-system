using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.Contracts.Base
{
    public interface ILedgerTransactionsDataStore : IDataStore<LedgerTransaction>
    {
        /// <summary>
        /// Get ledger transactions by organization id and cargo id
        /// </summary>
        /// <returns></returns>
        ICollection<LedgerTransaction> GetLedgerTransactions(int organizationId, int cargoId, int skip, int take);
    }
}

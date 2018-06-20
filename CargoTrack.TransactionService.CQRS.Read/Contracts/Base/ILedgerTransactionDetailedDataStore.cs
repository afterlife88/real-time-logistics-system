using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.Contracts.Base
{
    public interface ILedgerTransactionDetailedDataStore : IDataStore<LedgerTransactionDetailed>
    {
        /// <summary>
        /// Get ledger transactions by organization id and cargo id
        /// </summary>
        /// <returns></returns>
        LedgerTransactionDetailed GetLedgerTransaction(int ledgerTransactionId);
    }
}

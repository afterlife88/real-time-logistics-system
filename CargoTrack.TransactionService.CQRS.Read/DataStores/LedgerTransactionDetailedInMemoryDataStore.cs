using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.DataStores
{
    public class LedgerTransactionDetailedInMemoryDataStore : DataStore<LedgerTransactionDetailed>, ILedgerTransactionDetailedDataStore
    {
        /// <summary>
        /// Get a single ledger transaction
        /// </summary>
        /// <param name="ledgerTransactionId"></param>
        /// <returns></returns>
        public LedgerTransactionDetailed GetLedgerTransaction(int ledgerTransactionId)
        {
            return GetAll().SingleOrDefault(lt => lt.Id == ledgerTransactionId);
        }
    }
}

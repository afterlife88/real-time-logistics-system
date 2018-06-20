using System;
using System.Collections.Generic;
using CargoTrack.TransactionService.CQRS.Common.QueryFacades;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack
{
    public class QueryFacade : IQueryFacade
    {
        private readonly IBalanceDataStore _balanceDataStore;
        private readonly ILedgerTransactionsDataStore _ledgerTransactionsDataStore;
        private readonly ILedgerTransactionDetailedDataStore _ledgerTransactionDetailedDataStore;

        public QueryFacade(IBalanceDataStore balanceDataStore, ILedgerTransactionsDataStore ledgerTransactionsDataStore,
            ILedgerTransactionDetailedDataStore ledgerTransactionDetailedDataStore)
        {
            _balanceDataStore = balanceDataStore;
            _ledgerTransactionsDataStore = ledgerTransactionsDataStore;
            _ledgerTransactionDetailedDataStore = ledgerTransactionDetailedDataStore;
        }

        /// <summary>
        /// Get balance for an organization by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public ICollection<Balance> GetBalanceByOrganizationId(int organizationId)
        {
            return _balanceDataStore.GetByOrganizationId(organizationId);
        }

        /// <summary>
        /// Get balance for an organization by its kardex
        /// </summary>
        /// <param name="kardex"></param>
        /// <returns></returns>
        public ICollection<Balance> GetBalanceByKardex(string kardex)
        {
            return _balanceDataStore.GetByKardex(kardex);
        }

        /// <summary>
        /// Get ledger transactions
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="cargoId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public ICollection<LedgerTransaction> GetLedgerTransactions(int organizationId, int cargoId, int skip, int take)
        {
            return _ledgerTransactionsDataStore.GetLedgerTransactions(organizationId, cargoId, skip, take);
        }

        /// <summary>
        /// Get ledger transaction details
        /// </summary>
        /// <param name="ledgerTransactionId"></param>
        /// <returns></returns>
        public LedgerTransactionDetailed GetLedgerTransactionDetails(int ledgerTransactionId)
        {
            return _ledgerTransactionDetailedDataStore.GetLedgerTransaction(ledgerTransactionId);
        }
    }
}

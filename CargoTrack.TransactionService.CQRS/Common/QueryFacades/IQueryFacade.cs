using System;
using System.Collections.Generic;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Common.QueryFacades
{
    public interface IQueryFacade
    {
        ICollection<Balance> GetBalanceByOrganizationId(int organizationId);
        ICollection<Balance> GetBalanceByKardex(string kardex);
        ICollection<LedgerTransaction> GetLedgerTransactions(int organizationId, int cargoId, int skip, int take);

        LedgerTransactionDetailed GetLedgerTransactionDetails(int ledgerTransactionId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.Contracts.Base
{
    public interface IBalanceDataStore : IDataStore<Balance>
    {
        /// <summary>
        /// Get balance by organization id
        /// </summary>
        /// <returns></returns>
        ICollection<Balance> GetByOrganizationId(int organizationId);

        /// <summary>
        /// Get balance by Kardex
        /// </summary>
        /// <returns></returns>
        ICollection<Balance> GetByKardex(string kardex);

        Balance GetById(int balanceId);
    }
}

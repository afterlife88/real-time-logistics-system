using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.Entities;

namespace CargoTrack.TransactionService.CQRS.Read.DataStores
{
    public class BalanceInMemoryDataStore : DataStore<Balance>, IBalanceDataStore
    {
        /// <summary>
        /// Get balance by organization id
        /// </summary>
        /// <returns></returns>
        public ICollection<Balance> GetByOrganizationId(int id)
        {
            return GetAll().Where(b => b.OrganizationId == id).ToList();
        }

        /// <summary>
        /// Get balance by Kardex
        /// </summary>
        /// <returns></returns>
        public ICollection<Balance> GetByKardex(string kardex)
        {
            return GetAll().Where(b => b.Kardex.Equals(kardex, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public Balance GetById(int balanceId)
        {
            return GetAll().FirstOrDefault(r => r.Id == balanceId);
        }


    }
}

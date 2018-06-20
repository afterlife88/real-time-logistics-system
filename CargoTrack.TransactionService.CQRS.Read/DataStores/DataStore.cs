using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;

namespace CargoTrack.TransactionService.CQRS.Read.DataStores
{
    /// <summary>
    /// Generic in memory datastore
    /// </summary>
    /// <typeparam name="T">Type of entity for the repository.</typeparam>
    public class DataStore<T> : IDataStore<T> where T : class
    {
        private readonly ICollection<T> _data = new List<T>();

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            if (!_data.Contains(entity))
                _data.Add(entity);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public ICollection<T> GetAll()
        {
            return _data;
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            if (_data.Contains(entity))
            {
                _data.Remove(entity);
            }
        }
    }
}

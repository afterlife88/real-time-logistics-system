using System.Collections.Generic;

namespace CargoTrack.TransactionService.CQRS.Read.Contracts.Base
{
    public interface IDataStore<T>
    {
        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll();

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
    }
}

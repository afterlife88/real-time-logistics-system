using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;

namespace CargoTrack.TransactionService.CQRS.Write.Repositories.Base
{
    /// <summary>
    /// Generic repository based on EF
    /// </summary>
    /// <typeparam name="T">Type of entity for the repository.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// The EF DbContect used by the repository
        /// </summary>
        protected DbContext DbContext { get; set; }

        /// <summary>
        /// DbSet exposing the entities of the repository
        /// </summary>
        protected DbSet<T> DbSet { get; set; }

        #endregion

        /// <summary>
        /// Get all entities in the repository
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // not found; assume already deleted.
            Delete(entity);
        }
    }
}

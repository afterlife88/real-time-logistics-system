using System;
using System.Threading.Tasks;
using CargoTrack.OrderService.Data.Contracts.Base;

namespace CargoTrack.OrderService.Data
{
    /// <summary>
    /// Implementation of the unit of work design pattern based on EF
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Constructors / Destructors

        /// <summary>
        /// Default injectable constructor
        /// </summary>
        /// <param name="dataDbContext"></param>

        public UnitOfWork(DataDbContext dataDbContext)
        {
            DataDbContext = dataDbContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Db context
        /// </summary>
        public DataDbContext DataDbContext { get; set; }

        #endregion

        #region Repositories


        #endregion

        #region IUnitOfWork

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public async Task<int> CommitAsync()
        {
            return await DataDbContext.SaveChangesAsync();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataDbContext?.Dispose();
            }
        }
        #endregion
    }
}

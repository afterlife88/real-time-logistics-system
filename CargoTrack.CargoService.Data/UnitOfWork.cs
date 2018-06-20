using System;
using System.Threading.Tasks;
using CargoTrack.CargoService.Data.Contracts;
using CargoTrack.CargoService.Data.Contracts.Base;

namespace CargoTrack.CargoService.Data
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
        /// <param name="cargoTypeCategoryRepository"></param>
        /// <param name="cargoTypeRepository"></param>
        public UnitOfWork(DataDbContext dataDbContext, ICargoTypeCategoryRepository cargoTypeCategoryRepository, ICargoTypeRepository cargoTypeRepository)
        {
            DataDbContext = dataDbContext;
            CargoTypeCategories = cargoTypeCategoryRepository;
            CargoTypes = cargoTypeRepository;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Db context
        /// </summary>
        public DataDbContext DataDbContext { get; set; }

        #endregion

        #region Repositories
        /// <summary>
        /// Repository for cargo types categories
        /// </summary>
        public ICargoTypeCategoryRepository CargoTypeCategories { get; }

        /// <summary>
        /// Repository for cargo types
        /// </summary>
        public ICargoTypeRepository CargoTypes { get; }

        #endregion

        #region IUnitOfWork

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DataDbContext.SaveChanges();
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

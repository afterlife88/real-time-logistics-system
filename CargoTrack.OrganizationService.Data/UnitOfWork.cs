using System;
using System.Threading.Tasks;
using CargoTrack.OrganizationService.Data.Contracts;
using CargoTrack.OrganizationService.Data.Contracts.Base;

namespace CargoTrack.OrganizationService.Data
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
        /// <param name="organizationRepository"></param>
        /// <param name="organizationTypeRepository"></param>
        public UnitOfWork(DataDbContext dataDbContext,
            IOrganizationRepository organizationRepository, IOrganizationTypeRepository organizationTypeRepository)
        {
            DataDbContext = dataDbContext;
            OrganizationTypeRepository = organizationTypeRepository;
            OrganizationRepository = organizationRepository;
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
        /// Organization type repository
        /// </summary>
        public IOrganizationTypeRepository OrganizationTypeRepository { get; }

        /// <summary>
        /// Organization repository
        /// </summary>
        public IOrganizationRepository OrganizationRepository { get; }


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

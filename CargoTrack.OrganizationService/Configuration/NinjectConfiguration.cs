using System.Data.Entity;
using CargoTrack.OrganizationService.Data;
using CargoTrack.OrganizationService.Data.Configuration;
using CargoTrack.OrganizationService.Data.Contracts;
using CargoTrack.OrganizationService.Data.Contracts.Base;
using CargoTrack.OrganizationService.Data.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CargoTrack.OrganizationService.Configuration
{
    public class NinjectConfiguration : NinjectModule
    {
        /// <summary>
        /// Configure bindings for ninject
        /// </summary>
        public override void Load()
        {
            Bind<IDatabaseInitializer<DataDbContext>>().To<DatabaseInitializer>().InRequestScope();
            Bind<DataDbContext>().To<DataDbContext>().InRequestScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<IOrganizationTypeRepository>().To<OrganizationTypeRepository>().InRequestScope();
            Bind<IOrganizationRepository>().To<OrganizationRepository>().InRequestScope();
        }
    }
}

using System.Data.Entity;
using CargoTrack.CargoService.Data;
using CargoTrack.CargoService.Data.Configuration;
using CargoTrack.CargoService.Data.Contracts;
using CargoTrack.CargoService.Data.Contracts.Base;
using CargoTrack.CargoService.Data.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CargoTrack.CargoService.Configuration
{
    /// <summary>
    /// Configuration of ninject
    /// </summary>
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
            Bind<ICargoTypeCategoryRepository>().To<CargoTypeCategoryRepository>().InRequestScope();
            Bind<ICargoTypeRepository>().To<CargoTypeRepository>().InRequestScope();
        }
    }
}

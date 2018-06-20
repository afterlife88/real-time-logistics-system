using System.Data.Entity;
using CargoTrack.OrderService.Data;
using CargoTrack.OrderService.Data.Configuration;
using CargoTrack.OrderService.Data.Contracts.Base;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CargoTrack.OrderService.Configuration
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
        }
    }
}

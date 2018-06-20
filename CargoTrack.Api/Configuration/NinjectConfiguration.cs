using CargoTrack.CargoService.Contracts;
using CargoTrack.OrderService.Contracts;
using CargoTrack.OrganizationService.Contracts;
using CargoTrack.TransactionService.Contracts;
using Ninject.Modules;
using Ninject.Extensions.Wcf.Client;

namespace CargoTrack.Api.Configuration
{
    /// <summary>
	/// Ninject configuration
	/// </summary>
	public class NinjectConfiguration : NinjectModule
    {
        /// <summary>
        /// Configure bindings for ninject
        /// </summary>
        public override void Load()
        {
            // Bind to services
            Bind<ICargoService>().ToServiceChannel();
            Bind<IOrderService>().ToServiceChannel();
            Bind<IOrganizationService>().ToServiceChannel();
            Bind<ITransactionService>().ToServiceChannel();
        }
    }
}
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;

namespace CargoTrack.Api.Infrastructure
{
    /// <summary>
    /// Ninject hub activator
    /// </summary>
    public class NinjectHubActivator : IHubActivator
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// Ninject hub activator
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectHubActivator(IKernel kernel)
        {
            _kernel = kernel;
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)_kernel.TryGet(descriptor.HubType);
        }
    }
}
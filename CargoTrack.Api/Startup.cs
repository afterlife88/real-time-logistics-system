using CargoTrack.Api.Configuration;
using CargoTrack.Api.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(CargoTrack.Api.Startup))]

namespace CargoTrack.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // Setup Ninject
            var kernel = new StandardKernel(new NinjectConfiguration());

            var ninjectHubActivator = new NinjectHubActivator(kernel);

            GlobalHost.DependencyResolver.Register(
                typeof(IHubActivator),
                () => ninjectHubActivator);

            // Setup signalr
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    Resolver = GlobalHost.DependencyResolver
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}

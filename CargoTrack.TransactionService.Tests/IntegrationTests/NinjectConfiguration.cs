using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.TransactionService.Contracts;
using Ninject.Extensions.Wcf.Client;
using Ninject.Modules;

namespace CargoTrack.TransactionService.Tests.IntegrationTests
{
    /// <summary>
    /// Configure bindings for ninject
    /// </summary>
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITransactionService>().ToServiceChannel();
        }
    }
}

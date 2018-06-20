using System.Data.Entity;
using CargoTrack.CargoService.Contracts;
using CargoTrack.OrganizationService.Contracts;
using CargoTrack.TransactionService.CQRS.Common.Commands;
using CargoTrack.TransactionService.CQRS.Common.Events;
using CargoTrack.TransactionService.CQRS.Common.QueryFacades;
using CargoTrack.TransactionService.CQRS.Read.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Read.DataStores;
using CargoTrack.TransactionService.CQRS.ReadStack;
using CargoTrack.TransactionService.CQRS.Write;
using CargoTrack.TransactionService.CQRS.Write.Configuration;
using CargoTrack.TransactionService.CQRS.Write.Contracts;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Repositories;
using CargoTrack.TransactionService.CQRS.WriteStack.TransactionDecider;
using Ninject.Extensions.Wcf.Client;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CargoTrack.TransactionService.Configuration
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

            // Repos for write
            Bind<ITransactionRepository>().To<TransactionRepository>().InRequestScope();
            Bind<ILedgerTransactionRepository>().To<LedgerTransactionRepository>().InRequestScope();
            Bind<IBalanceRepository>().To<BalanceRepository>().InRequestScope();

            // CQRS
            Bind<IQueryFacade>().To<QueryFacade>().InRequestScope();
            Bind<ICommandBus>().To<CommandBus>().InRequestScope();
            Bind<IEventBus>().To<EventBus>().InRequestScope();

            // Read repos
            Bind<IBalanceDataStore>().To<BalanceInMemoryDataStore>().InSingletonScope();
            Bind<ILedgerTransactionDetailedDataStore>().To<LedgerTransactionDetailedInMemoryDataStore>().InSingletonScope();
            Bind<ILedgerTransactionsDataStore>().To<LedgerTransactionsInMemoryDataStore>().InSingletonScope();

            // Services
            Bind<IOrganizationService>().ToServiceChannel();
            Bind<ICargoService>().ToServiceChannel();

            // Utils
            Bind<ITransactionProcessor>().To<TransactionProcessor>().InRequestScope();
            Bind<ITransactionEngine>().To<TransactionEngine>().InRequestScope();
        }
    }
}

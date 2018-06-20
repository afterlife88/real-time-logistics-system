using System.Collections.Generic;
using CargoTrack.TransactionService.Contracts;
using CargoTrack.TransactionService.Contracts.Models.DTO.Commands;
using CargoTrack.TransactionService.Contracts.Models.Service.Commands;
using Ninject;
using NUnit.Framework;

namespace CargoTrack.TransactionService.Tests.IntegrationTests
{
    [TestFixture]
    public class TransactionServiceIntegrationTests
    {
        #region Variables 

        private static IKernel _kernel;

        #endregion

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel(new NinjectConfiguration());
        }

        /// <summary>
        /// Sample transaction service for pushing test data for transactions
        /// </summary>
        [Test]
        public void AddTransactions_Successful()
        {
            var service = _kernel.Get<ITransactionService>();

            foreach (var item in BunchOfTransactions())
            {
                var response = service.AddTransaction(new AddTransactionCommandRequest() { AddTransactionCommand = item });
                Assert.AreEqual(response.ErrorMessage, string.Empty);
            }


        }

        /// <summary>
        /// Test data
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AddTransactionCommandDTO> BunchOfTransactions()
        {
            return new List<AddTransactionCommandDTO>
            {
                new AddTransactionCommandDTO()
                {
                    Amount = 120,
                    CargoId = 1,
                    Comments = "From warehouse (org-id 3) to store (org-id 1)",
                    SourceOrganizationId = 3,
                    TargetOrganizationId = 1,
                    TransactionType = 1
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 140,
                    CargoId = 2,
                    Comments = "From warehouse (org-id 3) to store (org-id 2)",
                    SourceOrganizationId = 3,
                    TargetOrganizationId = 2,
                    TransactionType = 1
                },
                new AddTransactionCommandDTO()
                {
                    Amount = -150,
                    CargoId = 2,
                    Comments = "Transaction with cargo 2 for status correction transaction",
                    SourceOrganizationId = 1,
                    TransactionType = 3
                },
                 new AddTransactionCommandDTO()
                {
                    Amount = 99,
                    CargoId = 1,
                    Comments = "From supplier (org-id 5) to warehouse (org-id 3)",
                    SourceOrganizationId = 5,
                    TargetOrganizationId = 3,
                    TransactionType = 2
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 250,
                    CargoId = 3,
                    Comments = "Trash cargo for 4 organization",
                    SourceOrganizationId = 4,
                    TransactionType = 7,
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 100,
                    CargoId = 3,
                    Comments = "Trash cargo for 1 organization",
                    SourceOrganizationId = 1,
                    TransactionType = 7,
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 1000,
                    CargoId = 3,
                    Comments = "Sell cargo from warehouse",
                    SourceOrganizationId = 3,
                    TransactionType = 5
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 300,
                    CargoId = 1,
                    Comments = "Buy cargo for warehouse",
                    TargetOrganizationId = 3,
                    TransactionType = 4
                },
                new AddTransactionCommandDTO()
                {
                    Amount = 333,
                    TargetOrganizationId = 4,
                    SourceOrganizationId = 5,
                    TransactionType = 6,
                    CargoId = 3,
                    Comments = "Manual transfer from org-id 4 to org-id 5 with cargo 3"
                }

            };
        }
    }
}

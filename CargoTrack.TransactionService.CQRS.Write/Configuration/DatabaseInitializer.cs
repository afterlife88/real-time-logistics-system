using System;
using System.Collections.Generic;
using System.Data.Entity;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.Write.Configuration
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataDbContext>
    {
        #region Method Overrides 

        protected override void Seed(DataDbContext context)
        {
            context.SaveChanges();
        }
        #endregion
    }
}

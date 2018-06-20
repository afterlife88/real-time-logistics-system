using System.Data.Entity;

namespace CargoTrack.OrderService.Data.Configuration
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataDbContext>
    {
        #region Method Overrides 

        /// <summary>
        /// Seed database
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeDatabase(DataDbContext context)
        {
            base.InitializeDatabase(context);
        }
        #endregion
    }
}

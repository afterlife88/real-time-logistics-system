using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TrackerEnabledDbContext;

namespace CargoTrack.OrderService.Data
{
    public class DataDbContext : TrackerContext
    {
        #region Constructors / Destructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="databaseInitializer"></param>
        public DataDbContext(IDatabaseInitializer<DataDbContext> databaseInitializer)
            : base("Away4")
        {
            Database.SetInitializer(databaseInitializer);
        }

        #endregion

        #region Method overrides

        /// <summary>
        /// Configuration before models are createdd
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion

        #region DbSets


        #endregion
    }
}


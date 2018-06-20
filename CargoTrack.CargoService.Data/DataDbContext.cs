using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CargoTrack.CargoService.Data.Entities;
using TrackerEnabledDbContext;

namespace CargoTrack.CargoService.Data
{
    /// <summary>
    /// Specialized DbContext with Audit
    /// </summary>
    public class DataDbContext : TrackerContext
    {
        #region Constructors / Destructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="databaseInitializer"></param>
        public DataDbContext(IDatabaseInitializer<DataDbContext> databaseInitializer)
            : base("CargoTrack")
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
        public DbSet<CargoType> CargoTypes { get; set; }
        public DbSet<CargoTypeCategory> CargoTypeCategories { get; set; }

        #endregion

    }
}


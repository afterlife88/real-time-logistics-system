using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CargoTrack.OrganizationService.Data.Entities;

namespace CargoTrack.OrganizationService.Data
{
    public class DataDbContext : DbContext
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

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }

        #endregion

    }
}


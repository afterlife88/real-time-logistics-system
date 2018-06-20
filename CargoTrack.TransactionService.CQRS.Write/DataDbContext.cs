using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.Write
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

        public DbSet<Balance> Balances { get; set; }
        public DbSet<LedgerTransaction> LedgerTransactions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion
    }
}


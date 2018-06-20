using System.Data.Entity;
using CargoTrack.CargoService.Data.Entities;

namespace CargoTrack.CargoService.Data.Configuration
{
    /// <summary>
    /// Database initialization
    /// </summary>
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataDbContext>
    {
        #region Method Overrides 

        /// <summary>
        /// Seed the database with test data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataDbContext context)
        {
            var dryGood = AddCargoTypeCategory("Dry Goods", context);
            var freshGood = AddCargoTypeCategory("Fresh Goods", context);

            AddCargoType("Euro Pallet", "EUR", "8485838475734", 120, false, "Standard EURO Pallet",
                dryGood, context);

            AddCargoType("1/2 Euro Pallet", "HP", "8485838475735", 60, false,
                "Standard EURO 1/2 Pallet", dryGood, context);

            AddCargoType("Rolling Cage", "RB", "8458584387477", 2000, false,
                "Custom Rolling Cage for fresh goods", freshGood,
                context);

            context.SaveChanges();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Add cargo type category
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private CargoTypeCategory AddCargoTypeCategory(string name, DataDbContext context)
        {
            var cargoTypeCategory = new CargoTypeCategory()
            {
                Name = name,
            };
            // Add to context
            context.CargoTypeCategories.Add(cargoTypeCategory);
            return cargoTypeCategory;
        }

        /// <summary>
        /// Add cargo type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="abbreviation"></param>
        /// <param name="ean"></param>
        /// <param name="price"></param>
        /// <param name="leased"></param>
        /// <param name="description"></param>
        /// <param name="cargoTypeCategory"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static void AddCargoType(string name, string abbreviation, string ean, double price, bool leased,
            string description, CargoTypeCategory cargoTypeCategory, DataDbContext context)
        {
            var cargoType = new CargoType()
            {
                Abbreviation = abbreviation,
                Category = cargoTypeCategory,
                Description = description,
                Ean = ean,
                Leased = leased,
                Price = price,
                Name = name
            };

            context.CargoTypes.Add(cargoType);
        }
        #endregion
    }
}

using System.Data.Entity;
using CargoTrack.OrganizationService.Data.Entities;

namespace CargoTrack.OrganizationService.Data.Configuration
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataDbContext>
    {

        #region Method Overrides 

        /// <summary>
        /// Seed the database with test data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataDbContext context)
        {
            var store = AddOrganizationType("Store", context);
            var warehouse = AddOrganizationType("Warehouse", context);
            var transporter = AddOrganizationType("Transporter", context);
            var supplier = AddOrganizationType("Supplier", context);

            AddOrganization("Rozetka", "4050394", "10203040", "Stepan Bandery Avenue, 6", "02000", "Kiev",
                "UA", 50.487545, 30.494055, store, context);
            AddOrganization("Comfy", "49384838", "10203041", "Vadym Hetman Str. 6", "02000",
                "Kiev", "UA", 50.451034, 30.440797, store, context);
            AddOrganization("Atlantyk", "20304050", "10203042", "Stolichne highway, 100",
                "02000", "Kiev", "UA", 50.348114, 30.544554, warehouse, context);
            AddOrganization("Europcar", "68584737", "10203050", "Medova Str. 2", "02000", "Kiev",
                "UA", 0, 0, transporter, context);
            AddOrganization("Roshen", "58383848", "10203090", "Lyatoshinskogo Str. 14", "02000",
                "Kiev", "UA", 0, 0, supplier, context);

            context.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Add organizationtype
        /// </summary>
        /// <param name="name"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private OrganizationType AddOrganizationType(string name, DataDbContext context)
        {
            var organizationType = new OrganizationType()
            {
                Name = name,
            };
            // Add to context
            context.OrganizationTypes.Add(organizationType);
            return organizationType;
        }

        /// <summary>
        /// Add organization
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cvr"></param>
        /// <param name="kardex"></param>
        /// <param name="address"></param>
        /// <param name="zip"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="longitude"></param>
        /// <param name="lattitude"></param>
        /// <param name="organizationType"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private Organization AddOrganization(string name, string cvr, string kardex, string address, string zip,
            string city, string country, double longitude,
            double lattitude, OrganizationType organizationType, DataDbContext context)
        {
            var organization = new Organization()
            {
                Name = name,
                Address = address,
                City = city,
                Country = country,
                Cvr = cvr,
                Kardex = kardex,
                Lattitude = lattitude,
                Longitude = longitude,
                OrganizationType = organizationType,
                Zipcode = zip,
            };

            // Add to context
            context.Organizations.Add(organization);
            return organization;
        }
    }
}

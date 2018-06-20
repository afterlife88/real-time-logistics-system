using CargoTrack.OrganizationService.Data.Contracts;
using CargoTrack.OrganizationService.Data.Entities;
using CargoTrack.OrganizationService.Data.Repositories.Base;

namespace CargoTrack.OrganizationService.Data.Repositories
{
    public class OrganizationTypeRepository : Repository<OrganizationType>, IOrganizationTypeRepository
    {
        public OrganizationTypeRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }
    }
}

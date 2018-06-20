using System.Data.Entity;
using System.Linq;
using CargoTrack.TransactionService.CQRS.Write.Contracts;
using CargoTrack.TransactionService.CQRS.Write.Entities;
using CargoTrack.TransactionService.CQRS.Write.Repositories.Base;

namespace CargoTrack.TransactionService.CQRS.Write.Repositories
{
    public class BalanceRepository : Repository<Balance>, IBalanceRepository
    {
        public BalanceRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }

        public Balance GetBalanceByOrganizationId(int organizationId)
        {
            return DbSet.FirstOrDefault(r => r.OrganizationId == organizationId);
        }

        public Balance GetBalanceByOrganizationAndCargoId(int organizationId, int cargoId)
        {
            return DbSet.FirstOrDefault(r => r.OrganizationId == organizationId && r.CargoId == cargoId);
        }
    }
}

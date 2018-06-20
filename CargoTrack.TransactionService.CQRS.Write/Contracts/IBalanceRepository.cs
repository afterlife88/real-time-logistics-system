using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.Write.Contracts
{
    public interface IBalanceRepository : IRepository<Balance>
    {
        Balance GetBalanceByOrganizationId(int organizationId);
        Balance GetBalanceByOrganizationAndCargoId(int organizationId, int cargoId);
    }
}

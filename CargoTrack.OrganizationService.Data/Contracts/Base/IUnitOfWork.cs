using System.Threading.Tasks;

namespace CargoTrack.OrganizationService.Data.Contracts.Base
{
    public interface IUnitOfWork
    {
        /// <summary>
		/// Save changes in database
		/// </summary>
		/// <returns></returns>
		Task<int> CommitAsync();
        IOrganizationTypeRepository OrganizationTypeRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
    }
}

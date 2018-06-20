using System.Threading.Tasks;

namespace CargoTrack.OrderService.Data.Contracts.Base
{
    public interface IUnitOfWork
    {
        /// <summary>
		/// Save changes in database
		/// </summary>
		/// <returns></returns>
		Task<int> CommitAsync();
    }
}

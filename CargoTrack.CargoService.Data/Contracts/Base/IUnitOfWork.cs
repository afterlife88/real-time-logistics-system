using System.Threading.Tasks;

namespace CargoTrack.CargoService.Data.Contracts.Base
{
    /// <summary>
    /// Interface for generic unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes in database
        /// </summary>
        /// <returns></returns>
        void Commit();
        ICargoTypeCategoryRepository CargoTypeCategories { get; }
        ICargoTypeRepository CargoTypes { get; }
    }
}

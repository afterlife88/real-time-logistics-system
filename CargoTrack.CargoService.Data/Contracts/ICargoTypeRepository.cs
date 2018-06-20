using System.Linq;
using System.Threading.Tasks;
using CargoTrack.CargoService.Data.Contracts.Base;
using CargoTrack.CargoService.Data.Entities;

namespace CargoTrack.CargoService.Data.Contracts
{
    /// <summary>
    /// Interface for cargo type repository
    /// </summary>
    public interface ICargoTypeRepository : IRepository<CargoType>
    {
        /// <summary>
        /// Get a cargo type by Id 
        /// </summary>
        /// <param name="cargoTypeId"></param>
        /// <returns></returns>
        CargoType GetCargoTypeById(int cargoTypeId);

        int AddCargoType(CargoType model);
    }
}

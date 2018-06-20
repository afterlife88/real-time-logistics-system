using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CargoTrack.CargoService.Data.Contracts;
using CargoTrack.CargoService.Data.Entities;
using CargoTrack.CargoService.Data.Repositories.Base;

namespace CargoTrack.CargoService.Data.Repositories
{
    public class CargoTypeRepository : Repository<CargoType>, ICargoTypeRepository
    {
        #region Constructors
        public CargoTypeRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }
        #endregion

        /// <summary>
        /// Get all cargo types by its id
        /// </summary>
        /// <param name="cargoTypeId"></param>
        /// <returns></returns>
        public CargoType GetCargoTypeById(int cargoTypeId)
        {
            return GetAll().Include(r => r.Category).FirstOrDefault(r => r.Id == cargoTypeId);
        }

        /// <summary>
        /// Add a cargo type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddCargoType(CargoType model)
        {
            return DbSet.Add(model).Id;
        }
    }
}

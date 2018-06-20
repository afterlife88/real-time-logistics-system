using CargoTrack.CargoService.Data.Contracts;
using CargoTrack.CargoService.Data.Entities;
using CargoTrack.CargoService.Data.Repositories.Base;

namespace CargoTrack.CargoService.Data.Repositories
{
    public class CargoTypeCategoryRepository : Repository<CargoTypeCategory>, ICargoTypeCategoryRepository
    {
        public CargoTypeCategoryRepository(DataDbContext dataDbContext) : base(dataDbContext)
        { }
    }
}

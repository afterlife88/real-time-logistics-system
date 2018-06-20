using System.ServiceModel;
using System.Threading.Tasks;
using CargoTrack.CargoService.Contracts.Models.Service.Cargo;

namespace CargoTrack.CargoService.Contracts
{
    [ServiceContract]
    public interface ICargoService
    {
        [OperationContract]
        CreateCargoResponse CreateCargo(CreateCargoRequest request);

        [OperationContract]
        DeleteCargoResponse DeleteCargo(DeleteCargoRequest request);

        [OperationContract]
        GetCargoByIdResponse GetCargoById(GetCargoByIdRequest request);

        [OperationContract]
        ListCargoResponse ListCargo(ListCargoRequest request);

        [OperationContract]
        UpdateCargoResponse UpdateCargo(UpdateCargoRequest request);
    }
}

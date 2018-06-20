using System.ServiceModel;

namespace CargoTrack.OrderService.Contracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        void DoWork();
    }
}

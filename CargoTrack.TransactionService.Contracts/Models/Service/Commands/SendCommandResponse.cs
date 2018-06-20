using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Commands
{
    [DataContract]
    public class AddTransactionCommandResponse : ServiceResponse
    {
        public AddTransactionCommandResponse(ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

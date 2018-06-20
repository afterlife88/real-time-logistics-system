using System.Runtime.Serialization;
using CargoTrack.TransactionService.Contracts.Models.DTO.Commands;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Commands
{
    [DataContract]
    public class AddTransactionCommandRequest : ServiceRequest
    {
        [DataMember]
        public AddTransactionCommandDTO AddTransactionCommand { get; set; }
    }
}

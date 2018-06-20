using CargoTrack.Api.Models.DTO;

namespace CargoTrack.Api.Models.Service.Transaction
{
    public class AddTransactionRequest : ServiceRequest
    {
        public AddTransactionCommandDTO AddTransaction { get; set; }
    }
}
namespace CargoTrack.Api.Models.Service.Transaction
{
    public class AddTransactionResponse : ServiceResponse
    {
        public AddTransactionResponse(ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        { }
    }
}
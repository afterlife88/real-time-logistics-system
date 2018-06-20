namespace CargoTrack.Api.Models.Service.Transaction
{
    public class GetLedgerTransactionDetailsRequest : ServiceRequest
    {
        public int LedgerTransactionId { get; set; }
    }
}
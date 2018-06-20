namespace CargoTrack.Api.Models.Service.Transaction
{
    public class GetLedgerTransactionsRequest : ServiceRequest
    {
        public int OrganizationId { get; set; }
        public int CargoId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
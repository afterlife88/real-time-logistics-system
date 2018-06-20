namespace CargoTrack.Api.Models.Service.Balance
{
    public class GetBalanceByOrganizationIdRequest : ServiceRequest
    {
        public int OrganizationId { get; set; }
    }
}
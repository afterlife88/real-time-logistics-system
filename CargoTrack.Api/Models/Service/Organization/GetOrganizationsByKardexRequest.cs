namespace CargoTrack.Api.Models.Service.Organization
{
    public class GetOrganizationsByKardexRequest : ServiceRequest
    {
        public string Kardex { get; set; }
    }
}
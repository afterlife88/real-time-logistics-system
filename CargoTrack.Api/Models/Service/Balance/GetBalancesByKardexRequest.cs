namespace CargoTrack.Api.Models.Service.Balance
{
    public class GetBalancesByKardexRequest : ServiceRequest
    {
        public string Kardex { get; set; }
    }
}
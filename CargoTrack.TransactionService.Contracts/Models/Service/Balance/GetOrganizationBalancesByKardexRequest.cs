using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Balance
{
    [DataContract]
    public class GetOrganizationBalancesByKardexRequest : ServiceRequest
    {
        [DataMember]
        public string Kardex { get; set; }
    }
}

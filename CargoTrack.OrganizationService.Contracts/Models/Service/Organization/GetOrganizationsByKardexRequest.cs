using System.Runtime.Serialization;

namespace CargoTrack.OrganizationService.Contracts.Models.Service.Organization
{
    [DataContract]
    public class GetOrganizationsByKardexRequest : ServiceRequest
    {
        [DataMember]
        public string Kardex { get; set; }
    }
}

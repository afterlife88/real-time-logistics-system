using System.Runtime.Serialization;

namespace CargoTrack.OrganizationService.Contracts.Models.Service.Organization
{
    [DataContract]
    public class GetOrganizationByIdRequest : ServiceRequest
    {
        [DataMember]
        public int Id { get; set; }
    }
}

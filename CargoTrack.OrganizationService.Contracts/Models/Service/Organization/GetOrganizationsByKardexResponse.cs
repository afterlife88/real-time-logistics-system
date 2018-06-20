using System.Collections.Generic;
using System.Runtime.Serialization;
using CargoTrack.OrganizationService.Contracts.Models.DTO;

namespace CargoTrack.OrganizationService.Contracts.Models.Service.Organization
{
    [DataContract]
    public class GetOrganizationsByKardexResponse : ServiceResponse
    {
        [DataMember]
        public ICollection<OrganizationDTO> Organizations { get; set; }
        public GetOrganizationsByKardexResponse(ICollection<OrganizationDTO> organization, ServiceStatus serviceStatus, string errorMessage)
            : base(serviceStatus, errorMessage)
        {
            Organizations = organization;
        }
    }
}

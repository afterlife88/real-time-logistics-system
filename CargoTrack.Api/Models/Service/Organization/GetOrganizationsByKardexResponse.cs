using System.Collections.Generic;
using CargoTrack.Api.Models.DTO;

namespace CargoTrack.Api.Models.Service.Organization
{
    public class GetOrganizationsByKardexResponse : ServiceResponse
    {
        public ICollection<OrganizationDTO> Organizations { get; set; }

        public GetOrganizationsByKardexResponse(ICollection<OrganizationDTO> organizations, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            Organizations = organizations;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}
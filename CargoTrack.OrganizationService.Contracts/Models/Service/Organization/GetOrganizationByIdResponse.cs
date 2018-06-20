using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.OrganizationService.Contracts.Models.DTO;

namespace CargoTrack.OrganizationService.Contracts.Models.Service.Organization
{
    [DataContract]
    public class GetOrganizationByIdResponse : ServiceResponse
    {
        [DataMember]
        public OrganizationDetailedDTO OrganizationDetailed { get; set; }

        public GetOrganizationByIdResponse(OrganizationDetailedDTO organizationDetailed, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            OrganizationDetailed = organizationDetailed;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

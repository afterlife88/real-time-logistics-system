using System.ServiceModel;
using CargoTrack.OrganizationService.Contracts.Models.Service.Organization;

namespace CargoTrack.OrganizationService.Contracts
{
    [ServiceContract]
    public interface IOrganizationService
    {
        [OperationContract]
        GetOrganizationByIdResponse GetOrganizationById(GetOrganizationByIdRequest request);

        [OperationContract]
        GetOrganizationsByKardexResponse GetOrganizationsByKardex(GetOrganizationsByKardexRequest request);
    }
}

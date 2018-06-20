using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Balance
{
    [DataContract]
    public class GetOrganizationBalancesByIdRequest : ServiceRequest
    {
        [DataMember]
        public int OrganizationId { get; set; }
    }
}

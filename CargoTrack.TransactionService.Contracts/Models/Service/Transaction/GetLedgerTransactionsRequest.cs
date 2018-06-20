using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Transaction
{
    [DataContract]
    public class GetLedgerTransactionsRequest : ServiceRequest
    {
        [DataMember]
        public int OrganizationId { get; set; }
        [DataMember]
        public int CargoId { get; set; }
        [DataMember]
        public int Skip { get; set; }
        [DataMember]
        public int Take { get; set; }
    }
}

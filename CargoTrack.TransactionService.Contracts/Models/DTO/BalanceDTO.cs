using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.DTO
{
    [DataContract]
    public class BalanceDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Kardex { get; set; }
        [DataMember]
        public string Ean { get; set; }
        [DataMember]
        public int OrganizationId { get; set; }
        [DataMember]
        public int CargoBalance { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CargoId { get; set; }
    }
}

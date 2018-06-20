using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.DTO
{
    [DataContract]
    public class LedgerTransactionDetailedDTO : LedgerTransactionDTO
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Ean { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string Kardex { get; set; }
    }
}
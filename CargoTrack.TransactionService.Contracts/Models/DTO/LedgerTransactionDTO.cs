using System;
using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.DTO
{
    [DataContract]
    public class LedgerTransactionDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CargoId { get; set; }
        [DataMember]
        public int OrganizationId { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }
        [DataMember]
        public int TransactionType { get; set; }
        [DataMember]
        public int Amount { get; set; }
        [DataMember]
        public double TotalPrice { get; set; }
    }
}

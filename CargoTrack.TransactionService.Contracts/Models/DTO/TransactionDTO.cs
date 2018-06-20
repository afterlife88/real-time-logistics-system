using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.DTO
{
    [DataContract]
    public class TransactionDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }
        [DataMember]
        public int TransactionType { get; set; }
        [DataMember]
        public int Amount { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public int SourceOrganizationId { get; set; }
        [DataMember]
        public int TargetOrganizationId { get; set; }
        [DataMember]
        public int CargoId { get; set; }
        [DataMember]
        public virtual ICollection<LedgerTransactionDTO> LedgerTransactions { get; set; }
    }
}

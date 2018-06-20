using System;
using System.Collections.Generic;

namespace CargoTrack.TransactionService.CQRS.Write.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public string Comments { get; set; }
        public int? SourceOrganizationId { get; set; }
        public int? TargetOrganizationId { get; set; }
        public int CargoId { get; set; }
        public virtual ICollection<LedgerTransaction> LedgerTransactions { get; set; }
    }
}

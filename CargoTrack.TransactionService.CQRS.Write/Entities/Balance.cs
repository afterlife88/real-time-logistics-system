using System;
using System.ComponentModel.DataAnnotations;

namespace CargoTrack.TransactionService.CQRS.Write.Entities
{
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdatedLedgerTransactionId { get; set; }
        public int CargoBalance { get; set; }
    }
}

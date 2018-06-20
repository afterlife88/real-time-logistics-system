using System;

namespace CargoTrack.TransactionService.CQRS.Read.Entities
{
    public class LedgerTransaction
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime Timestamp { get; set; }
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
    }
}

using System;

namespace CargoTrack.Api.Models.DTO
{
    /// <summary>
    /// DTO for basic information about a ledger transaction
    /// </summary>
    public class LedgerTransactionDTO
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
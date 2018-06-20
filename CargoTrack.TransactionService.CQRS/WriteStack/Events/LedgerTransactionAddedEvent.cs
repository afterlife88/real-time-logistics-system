using System;
using CargoTrack.TransactionService.CQRS.Common.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Events
{
    /// <summary>
    /// Event fired when a ledger transaction is added
    /// </summary>
    public class LedgerTransactionAddedEvent : Event
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime Timestamp { get; set; }
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; } = "TestUserId";
        public string UserName { get; set; } = "TestUserName";
        public string Ean { get; set; }
        public string Comments { get; set; }
        public string Kardex { get; set; }
    }
}

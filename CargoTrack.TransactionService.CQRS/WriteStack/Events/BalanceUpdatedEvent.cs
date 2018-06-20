using CargoTrack.TransactionService.CQRS.Common.Events;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Events
{
    /// <summary>
    /// Event fired when a balance is updated
    /// </summary>
    public class BalanceUpdatedEvent : Event
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Kardex { get; set; }
        public string Ean { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int CargoId { get; set; }
    }
}

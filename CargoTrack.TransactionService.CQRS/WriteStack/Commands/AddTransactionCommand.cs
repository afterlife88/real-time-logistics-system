using CargoTrack.TransactionService.CQRS.Common.Commands;

namespace CargoTrack.TransactionService.CQRS.WriteStack.Commands
{
    /// <summary>
    /// Command for adding a transaction
    /// </summary>
    public class AddTransactionCommand : Command
    {
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public string Comments { get; set; }
        public int? SourceOrganizationId { get; set; }
        public int? TargetOrganizationId { get; set; }
        public int CargoId { get; set; }
    }
}

namespace CargoTrack.Api.Models.DTO
{
    /// <summary>
    /// Command for adding a new transaction
    /// </summary>
    public class AddTransactionCommandDTO
    {
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public string Comments { get; set; }
        public int? SourceOrganizationId { get; set; }
        public int? TargetOrganizationId { get; set; }
        public int CargoId { get; set; }
    }
}
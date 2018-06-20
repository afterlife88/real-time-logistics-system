namespace CargoTrack.TransactionService.CQRS.Write.Entities
{
    public class LedgerTransaction
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
    }
}

namespace CargoTrack.TransactionService.CQRS.Read.Entities
{
    public class Balance
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

namespace CargoTrack.Api.Models.DTO
{
    /// <summary>
    /// DTO for balances
    /// </summary>
    public class BalanceDTO
    {
        public int Id { get; set; }
        public string Kardex { get; set; }
        public string Ean { get; set; }
        public int OrganizationId { get; set; }
        public int CargoBalance { get; set; }
        public string Description { get; set; }
        public int CargoId { get; set; }
    }
}
namespace CargoTrack.Api.Models.DTO
{
    /// <summary>
    /// DTO for details about a ledger transaction
    /// </summary>
    public class LedgerTransactionDetailedDTO : LedgerTransactionDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Ean { get; set; }
        public string Comments { get; set; }
        public string Kardex { get; set; }
    }
}
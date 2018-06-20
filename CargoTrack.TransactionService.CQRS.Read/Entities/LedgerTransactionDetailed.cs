namespace CargoTrack.TransactionService.CQRS.Read.Entities
{
    /// <summary>
    /// Get a detailed ledger transaction
    /// </summary>
    public class LedgerTransactionDetailed : LedgerTransaction
    {
        //TODO: When identity will be created change it
        public string UserId { get; set; } = "TestUserId";
        public string UserName { get; set; } = "TestUserName";
        public string Ean { get; set; }
        public string Comments { get; set; }
        public string Kardex { get; set; }

    }
}

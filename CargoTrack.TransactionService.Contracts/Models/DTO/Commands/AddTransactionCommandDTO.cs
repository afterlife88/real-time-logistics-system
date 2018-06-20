using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.DTO.Commands
{
    /// <summary>
    /// Command for adding a new transaction
    /// 
    /// The following will happend when the command is executed by the handler:
    /// 
    /// 1. Validate the source organization exists and type
    /// 2. Validate the target organization exists and type
    /// 3. Validate the cargo exists and amount is positive unless stated in below transaction description
    /// 4. Add ledger transactions according to the transactiontype (See below)
    /// 5. Update the balance for the organization / cargo combination
    /// 
    /// Transactionstypes
    /// 
    /// ID  Description                     LedgerTransactions
    /// 01  Delivery to store               -Amount Source Organization (Warehouse)
    ///                                     +Amount Target Organization (Store)
    /// 02  Receive cargo from supplier     -Amount Source Organization (Supplier)
    ///                                     +Amount Target Organization (Warehouse)
    /// 03  Status correction               +Amount Source Organization (Any type - Negative amount allowed)
    /// 04  Buy cargo                       +Amount Target Organization (Warehouse)
    /// 05  Sell cargo                      -Amount Source Organization (Warehouse)
    /// 06  Manual transfer of cargo        +Amount Source Organization (Any type - Negative amount allowed)
    ///                                     -Amount Target Organization (Any type - Negative amount allowed)
    /// 07  Trash cargo                     -Amount Source Organization (Any type)
    /// </summary>
    [DataContract]
    public class AddTransactionCommandDTO : CommandDTO
    {
        [DataMember]
        public int TransactionType { get; set; }
        [DataMember]
        public int Amount { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public int? SourceOrganizationId { get; set; }
        [DataMember]
        public int? TargetOrganizationId { get; set; }
        [DataMember]
        public int CargoId { get; set; }
    }
}
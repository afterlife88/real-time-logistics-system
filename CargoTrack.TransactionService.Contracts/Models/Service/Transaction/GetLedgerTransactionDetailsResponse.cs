using System.Runtime.Serialization;
using CargoTrack.TransactionService.Contracts.Models.DTO;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Transaction
{
    [DataContract]
    public class GetLedgerTransactionDetailsResponse : ServiceResponse
    {
        [DataMember]
        public LedgerTransactionDetailedDTO LedgerTransaction { get; set; }

        public GetLedgerTransactionDetailsResponse(LedgerTransactionDetailedDTO ledgerTransaction, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            LedgerTransaction = ledgerTransaction;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

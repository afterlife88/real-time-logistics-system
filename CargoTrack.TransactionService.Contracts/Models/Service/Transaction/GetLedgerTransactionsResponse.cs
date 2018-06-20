using System.Collections.Generic;
using System.Runtime.Serialization;
using CargoTrack.TransactionService.Contracts.Models.DTO;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Transaction
{
    [DataContract]
    public class GetLedgerTransactionsResponse : ServiceResponse
    {
        [DataMember]
        public ICollection<LedgerTransactionDTO> LedgerTransactions { get; set; }

        public GetLedgerTransactionsResponse(ICollection<LedgerTransactionDTO> ledgerTransactions, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            LedgerTransactions = ledgerTransactions;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

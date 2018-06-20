using System.Collections.Generic;
using CargoTrack.Api.Models.DTO;

namespace CargoTrack.Api.Models.Service.Transaction
{
    public class GetLedgerTransactionsResponse : ServiceResponse
    {
        public ICollection<LedgerTransactionDTO> LedgerTransactions { get; set; }

        public GetLedgerTransactionsResponse(ICollection<LedgerTransactionDTO> ledgerTransactions, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            LedgerTransactions = ledgerTransactions;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}
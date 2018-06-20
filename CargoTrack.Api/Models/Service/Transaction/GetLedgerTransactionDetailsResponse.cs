using System.Collections.Generic;
using CargoTrack.Api.Models.DTO;

namespace CargoTrack.Api.Models.Service.Transaction
{
    public class GetLedgerTransactionDetailsResponse : ServiceResponse
    {
        public LedgerTransactionDetailedDTO LedgerTransaction { get; set; }

        public GetLedgerTransactionDetailsResponse(LedgerTransactionDetailedDTO ledgerTransaction, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            LedgerTransaction = ledgerTransaction;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}
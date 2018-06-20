using System;
using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Transaction
{
    [DataContract]
    public class GetLedgerTransactionDetailsRequest : ServiceRequest
    {
        [DataMember]
        public int LedgerTransactionId { get; set; }
    }
}

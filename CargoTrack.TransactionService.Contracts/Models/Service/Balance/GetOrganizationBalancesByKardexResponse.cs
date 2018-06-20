using System.Collections.Generic;
using System.Runtime.Serialization;
using CargoTrack.TransactionService.Contracts.Models.DTO;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Balance
{
    [DataContract]
    public class GetOrganizationBalancesByKardexResponse : ServiceResponse
    {
        [DataMember]
        public ICollection<BalanceDTO> Balances { get; set; }

        public GetOrganizationBalancesByKardexResponse(ICollection<BalanceDTO> balances, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            Balances = balances;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

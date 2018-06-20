using System.Collections.Generic;
using System.Runtime.Serialization;
using CargoTrack.TransactionService.Contracts.Models.DTO;

namespace CargoTrack.TransactionService.Contracts.Models.Service.Balance
{
    [DataContract]
    public class GetOrganizationBalancesByIdResponse : ServiceResponse
    {
        [DataMember]
        public ICollection<BalanceDTO> Balances { get; set; }

        public GetOrganizationBalancesByIdResponse(ICollection<BalanceDTO> balances, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            Balances = balances;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

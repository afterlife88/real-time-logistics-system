using System.Collections.Generic;
using CargoTrack.Api.Models.DTO;

namespace CargoTrack.Api.Models.Service.Balance
{
    public class GetBalanceByOrganizationIdResponse : ServiceResponse
    {
        public ICollection<BalanceDTO> Balances { get; set; }
        public GetBalanceByOrganizationIdResponse(ICollection<BalanceDTO> balances, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            Balances = balances;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}
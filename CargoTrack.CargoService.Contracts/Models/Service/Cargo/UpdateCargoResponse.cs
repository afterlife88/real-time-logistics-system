using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.CargoService.Contracts.Models.Service.Cargo
{
    [DataContract]
    public class UpdateCargoResponse : ServiceResponse
    {
        public UpdateCargoResponse(ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

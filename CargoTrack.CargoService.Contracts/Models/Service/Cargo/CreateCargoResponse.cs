using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.CargoService.Contracts.Models.Service.Cargo
{
    [DataContract]
    public class CreateCargoResponse : ServiceResponse
    {
        [DataMember]
        public int Id { get; set; }

        public CreateCargoResponse(int id, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            Id = id;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

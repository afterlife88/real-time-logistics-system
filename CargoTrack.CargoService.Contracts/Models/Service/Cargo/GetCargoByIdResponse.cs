using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CargoTrack.CargoService.Contracts.Models.DTO;

namespace CargoTrack.CargoService.Contracts.Models.Service.Cargo
{
    [DataContract]
    public class GetCargoByIdResponse : ServiceResponse
    {
        [DataMember]
        public CargoTypeDetailedDTO CargoTypeDetailed { get; set; }

        public GetCargoByIdResponse(CargoTypeDetailedDTO cargoTypeDetailed, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            CargoTypeDetailed = cargoTypeDetailed;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

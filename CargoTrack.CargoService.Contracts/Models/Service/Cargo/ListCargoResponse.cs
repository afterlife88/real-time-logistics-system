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
    public class ListCargoResponse : ServiceResponse
    {
        [DataMember]
        public ICollection<CargoTypeDTO> CargoTypes { get; set; }

        public ListCargoResponse(ICollection<CargoTypeDTO> cargoTypes, ServiceStatus serviceStatus, string errorMessage) : base(serviceStatus, errorMessage)
        {
            CargoTypes = cargoTypes;
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }
    }
}

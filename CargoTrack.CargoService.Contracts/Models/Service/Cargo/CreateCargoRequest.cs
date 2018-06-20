using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.CargoService.Contracts.Models.Service.Cargo
{
    [DataContract]
    public class CreateCargoRequest : ServiceRequest
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
        [DataMember]
        public string Ean { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public bool Leased { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Category { get; set; }
    }
}

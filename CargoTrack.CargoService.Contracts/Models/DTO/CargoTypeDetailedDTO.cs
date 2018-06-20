using System.Runtime.Serialization;

namespace CargoTrack.CargoService.Contracts.Models.DTO
{
    [DataContract]
    public class CargoTypeDetailedDTO : CargoTypeDTO
    {
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
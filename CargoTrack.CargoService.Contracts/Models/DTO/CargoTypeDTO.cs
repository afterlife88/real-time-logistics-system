using System.Runtime.Serialization;

namespace CargoTrack.CargoService.Contracts.Models.DTO
{
    [DataContract]
    public class CargoTypeDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
    }
}

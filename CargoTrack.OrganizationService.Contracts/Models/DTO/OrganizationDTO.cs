using System.Runtime.Serialization;

namespace CargoTrack.OrganizationService.Contracts.Models.DTO
{
    [DataContract]
    public class OrganizationDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Kardex { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Zipcode { get; set; }
        [DataMember]
        public string City { get; set; }
    }
}

using System.Runtime.Serialization;

namespace CargoTrack.OrganizationService.Contracts.Models.DTO
{
    [DataContract]
    public class OrganizationDetailedDTO : OrganizationDTO
    {
        [DataMember]
        public string Cvr { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Lattitude { get; set; }
        [DataMember]
        public string OrganizationType { get; set; }
    }
}
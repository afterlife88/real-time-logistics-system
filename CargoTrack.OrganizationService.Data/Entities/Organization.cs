namespace CargoTrack.OrganizationService.Data.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cvr { get; set; }
        public string Kardex { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
    }
}

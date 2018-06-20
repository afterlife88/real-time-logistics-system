namespace CargoTrack.Api.Models.DTO
{
    /// <summary>
    /// DTO for details about an organization
    /// </summary>
    public class OrganizationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Kardex { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
    }
}
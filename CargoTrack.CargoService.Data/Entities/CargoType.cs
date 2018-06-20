using System.ComponentModel.DataAnnotations;

namespace CargoTrack.CargoService.Data.Entities
{
    [TrackChanges]
    public class CargoType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Ean { get; set; }
        public double Price { get; set; }
        public bool Leased { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual CargoTypeCategory Category { get; set; }
    }
}

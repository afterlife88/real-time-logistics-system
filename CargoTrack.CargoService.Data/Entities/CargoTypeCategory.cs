using System.ComponentModel.DataAnnotations;

namespace CargoTrack.CargoService.Data.Entities
{
    [TrackChanges]
    public class CargoTypeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

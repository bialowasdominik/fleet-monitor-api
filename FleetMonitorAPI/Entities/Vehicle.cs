using System.ComponentModel.DataAnnotations.Schema;

namespace FleetMonitorAPI.Entities
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string? RegistrationNumber { get; set; }
        public long? DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}

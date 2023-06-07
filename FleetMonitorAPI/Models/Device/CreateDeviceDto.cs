using System.ComponentModel.DataAnnotations;

namespace FleetMonitorAPI.Models
{
    public class CreateDeviceDto
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int DriverId { get; set; }
    }
}

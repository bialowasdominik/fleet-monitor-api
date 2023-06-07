using System.ComponentModel.DataAnnotations;

namespace FleetMonitorAPI.Models
{
    public class UpdateDeviceDto
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int ? DriverId { get; set; }
    }
}

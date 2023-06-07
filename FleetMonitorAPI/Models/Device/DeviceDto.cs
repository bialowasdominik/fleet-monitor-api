using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Models.Position;

namespace FleetMonitorAPI.Models
{
    public class DeviceDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<PositionsDto> Positions { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace FleetMonitorAPI.Entities
{
    public class Device
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; } 
        public virtual Vehicle Vehicle { get; set; }
        public virtual List<Positions> Positions { get; set; }
    }
}

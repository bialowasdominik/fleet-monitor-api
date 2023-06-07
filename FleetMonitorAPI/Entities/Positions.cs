namespace FleetMonitorAPI.Entities
{
    public class Positions
    {
        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long DeviceId { get; set; }
        public DateTime Time { get; set; }
        public virtual Device Device { get; set; }
    }
}
namespace FleetMonitorAPI.Models.Position
{
    public class CreatePositionDto
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Time { get; set; }
        public long DeviceId { get; set; }
    }
}
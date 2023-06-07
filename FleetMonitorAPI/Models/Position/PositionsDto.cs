namespace FleetMonitorAPI.Models.Position
{
    public class PositionsDto
    {
        public long Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Time { get; set; }
    }
}

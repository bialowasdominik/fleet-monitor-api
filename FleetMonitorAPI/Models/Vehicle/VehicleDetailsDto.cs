namespace FleetMonitorAPI.Models.Vehicle
{
    public class VehicleDetailsDto
    {
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string? RegistrationNumber { get; set; }
        public long DeviceId { get; set; }
        public string? DeviceName { get; set;}
    }
}

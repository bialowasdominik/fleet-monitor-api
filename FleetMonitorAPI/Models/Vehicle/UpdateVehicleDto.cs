namespace FleetMonitorAPI.Models.Vehicle
{
    public class UpdateVehicleDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public long DeviceId { get; set; }
    }
}


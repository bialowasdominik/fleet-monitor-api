namespace FleetMonitorAPI.Models
{
    public class DeviceDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleVIN { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public long DriverId { get; set; }

    }
}

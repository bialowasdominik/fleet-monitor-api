namespace FleetMonitorAPI.Models.Driver
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<DeviceDto> Devices { get; set; }
    }
}

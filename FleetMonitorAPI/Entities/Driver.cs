namespace FleetMonitorAPI.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Device> Devices { get; set; }
    }
}

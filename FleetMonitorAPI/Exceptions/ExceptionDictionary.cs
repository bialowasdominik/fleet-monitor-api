namespace FleetMonitorAPI.Exceptions
{
    public static class ExceptionDictionary
    {
        public static readonly string DeviceNotFound = "Queried device not found";
        public static readonly string VehicleNotFound = "Queried vehicle not found";
        public static readonly string DriverNotFound = "Queried driver not found";
        public static readonly string TakenEmail = "That email is curlentrly in use";
        public static readonly string LoginException = "Invalid username or password";
        public static readonly string WrongItemRange = "Query recived wrong item per page range";
        public static readonly string SortIsOptional = "Sort is optional and must have column name in querry";
    }
}

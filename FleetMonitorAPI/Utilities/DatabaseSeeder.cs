using FleetMonitorAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetMonitorAPI.Utilities
{
    public class DatabaseSeeder
    {
        private readonly FleetMonitorDbContext _dbContext;
        public DatabaseSeeder(FleetMonitorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var migrations = _dbContext.Database.GetPendingMigrations();
                if (migrations != null && migrations.Any()) 
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRole();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Drivers.Any())
                {
                    var drivers = GetDrivers();
                    _dbContext.Drivers.AddRange(drivers);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Devices.Any())
                {
                    var devices = GetDevices();
                    _dbContext.Devices.AddRange(devices);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Vehicles.Any())
                {
                    var vehicles = GetVehicles();
                    _dbContext.Vehicles.AddRange(vehicles);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Driver> GetDrivers() 
        {
            var drivers = new List<Driver>() 
            {
                new Driver()
                {
                    FirstName = "Olaf",
                    LastName = "Kamiński"
                },
                new Driver()
                {
                    FirstName = "Mikołaj",
                    LastName = "Markiewicz"
                }
            };
            return drivers;
        }
        private IEnumerable<Vehicle> GetVehicles()
        {
            var vehicles = new List<Vehicle>()
            {
                new Vehicle()
                {
                    Brand = "Saab",
                    Model = "9-3",
                    VIN = "1N4AL11D96N331227",
                    RegistrationNumber = "ST47891",
                    DeviceId = 1,
                },
                new Vehicle()
                {
                    Brand = "Ford",
                    Model = "Mondeo",
                    VIN = "SCEGXUBC0GAVW6680",
                    RegistrationNumber = "POT1826",
                    DeviceId = 2,
                }
            };
            return vehicles;
        }
        private IEnumerable<Role> GetRole()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id= 1,
                    Name = "Admin",
                },
                new Role()
                {
                    Id= 2,
                    Name = "Driver",
                },
                new Role()
                {
                    Id= 3,
                    Name = "User",
                }
            };

            return roles;
        }
        private IEnumerable<Device> GetDevices()
        {
            var devices = new List<Device>()
            {
                new Device()
                {
                    Name = "Royal castle localisator",
                    Positions = new List<Positions>
                    {
                        new Positions()
                        {
                            Latitude = 52.24773591853942,
                            Longitude = 21.014667423020242,
                        }
                    },
                    DriverId = 1,
                },
                new Device()
                {
                    Name = "London localisator",
                    Positions = new List<Positions>
                    {
                        new Positions()
                        {
                            Latitude = 51.501425413912315,
                            Longitude = -0.1419225357325051,
                        },
                        new Positions()
                        {
                            Latitude = 51.501872614411184,
                            Longitude = -0.14061066854323057,
                        },
                        new Positions()
                        {
                            Latitude = 51.5004434213909,
                            Longitude = -0.13916422957196814,
                        },
                        new Positions()
                        {
                            Latitude = 51.50131641968576,
                            Longitude = -0.13052193317896918,
                        },
                        new Positions()
                        {
                            Latitude = 51.50087992261303,
                            Longitude = -0.12468443885447938,
                        },
                    },
                    DriverId = 1,
                }
            };

            return devices;
        }
    }
}

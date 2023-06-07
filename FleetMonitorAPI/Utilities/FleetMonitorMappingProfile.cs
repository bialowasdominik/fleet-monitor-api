using AutoMapper;
using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Driver;
using FleetMonitorAPI.Models.Login;
using FleetMonitorAPI.Models.Position;
using FleetMonitorAPI.Models.Vehicle;

namespace FleetMonitorAPI.Utilities
{
    public class FleetMonitorMappingProfile : Profile
    {
        public FleetMonitorMappingProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<Positions, PositionsDto>();
            CreateMap<CreateDeviceDto, Device>();
            CreateMap<CreatePositionDto, Positions>();
            CreateMap<User, LoginResponseDto>();
            CreateMap<Device, DeviceDetailsDto>()
                .ForMember(e => e.VehicleBrand, b => b.MapFrom(v => v.Vehicle.Brand))
                .ForMember(e => e.VehicleModel, b => b.MapFrom(v => v.Vehicle.Model))
                .ForMember(e => e.VehicleVIN, b => b.MapFrom(v => v.Vehicle.VIN))
                .ForMember(e => e.VehicleRegistrationNumber, b => b.MapFrom(v => v.Vehicle.RegistrationNumber))
                .ForMember(e => e.DriverFirstName, b => b.MapFrom(d => d.Driver.FirstName))
                .ForMember(e => e.DriverLastName, b => b.MapFrom(d => d.Driver.LastName))
                .ForMember(e => e.DriverId, b=>b.MapFrom(d => d.Driver.Id));
            CreateMap<Device,DevicePositionDto>()
                .ForMember(e => e.VehicleBrand, b => b.MapFrom(v => v.Vehicle.Brand))
                .ForMember(e => e.VehicleModel, b => b.MapFrom(v => v.Vehicle.Model))
                .ForMember(e => e.VehicleVIN, b => b.MapFrom(v => v.Vehicle.VIN))
                .ForMember(e => e.VehicleRegistrationNumber, b => b.MapFrom(v => v.Vehicle.RegistrationNumber))
                .ForMember(e => e.DriverFirstName, b => b.MapFrom(d => d.Driver.FirstName))
                .ForMember(e => e.DriverLastName, b => b.MapFrom(d => d.Driver.LastName));
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<Vehicle, VehicleListDto>()
                .ForMember(e => e.DeviceId, b => b.MapFrom(v => v.Device.Id));
            CreateMap<Vehicle,VehicleDetailsDto>()
                .ForMember(e => e.DeviceId, b=>b.MapFrom(v=>v.Device.Id))
                .ForMember(e=>e.DeviceName, b=>b.MapFrom(v=>v.Device.Name));
            CreateMap<Driver, DriverDto>();
            CreateMap<CreateDriverDto, Driver>();
        }
    }
}

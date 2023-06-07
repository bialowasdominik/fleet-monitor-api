using AutoMapper;
using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Models.Vehicle;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FleetMonitorAPI.Services
{
    public class VehicleService : IVehcileService
    {
        private readonly FleetMonitorDbContext _dbContext;
        private readonly IMapper _mapper;

        public VehicleService(FleetMonitorDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public long Create(CreateVehicleDto dto)
        {
            var vehicle = _mapper.Map<Vehicle>(dto);
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();
            return vehicle.Id;
        }

        public void Delete(int id)
        {
            var vehicle = _dbContext
                .Vehicles
                .FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                throw new NotFoundException(ExceptionDictionary.VehicleNotFound);

            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();
        }

        public IEnumerable<VehicleListDto> GetAll()
        {
            var vehicle = _dbContext
                .Vehicles
                .Include(v=>v.Device)
                .ToList();

            var result = _mapper.Map<IEnumerable<VehicleListDto>>(vehicle);
            return result;
        }

        public PageResult<VehicleDto> GetAllWithPagination(PaginationQuery query)
        {
            var baseQuery = _dbContext
                .Vehicles
                .Where(v => query.Phrase == null ||
                (v.Model.ToLower().Contains(query.Phrase.ToLower())) ||
                (v.Brand.ToLower().Contains(query.Phrase.ToLower())) ||
                (v.RegistrationNumber.ToLower().Contains(query.Phrase.ToLower())) ||
                (v.VIN.ToLower().Contains(query.Phrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Vehicle, object>>>()
                {
                    {nameof(Vehicle.Id), d => d.Id},
                    {nameof(Vehicle.Brand), d => d.Brand},
                    {nameof(Vehicle.Model), d => d.Model},
                    {nameof(Vehicle.RegistrationNumber), d => d.RegistrationNumber},
                    {nameof(Vehicle.VIN), d => d.VIN}
                };

                var selectedColumn = columnSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var vehicles = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItem = baseQuery.Count();
            var vehicleDtoList = _mapper.Map<List<VehicleDto>>(vehicles);
            var result = new PageResult<VehicleDto>(vehicleDtoList,totalItem,query.PageSize,query.PageNumber);
            return result;
        }

        public long GetAmountOfDevices()
        {
            var amount = _dbContext.Vehicles.Count();
            return amount;
        }

        public VehicleDetailsDto GetDetailsById(int id)
        {
            var vehcile = _dbContext
                .Vehicles
                .Include(v => v.Device)
                .FirstOrDefault(v => v.Id == id);
            var result = _mapper.Map<VehicleDetailsDto>(vehcile);
            return result;
        }

        public void Update(UpdateVehicleDto dto, long id)
        {
            var vehicle = _dbContext
                .Vehicles
                .FirstOrDefault(v => v.Id == id);

            if (vehicle is null)
                throw new NotFoundException(ExceptionDictionary.VehicleNotFound);

            vehicle.Brand = dto.Brand;
            vehicle.Model = dto.Model;
            vehicle.RegistrationNumber = dto.RegistrationNumber;
            vehicle.VIN = dto.VIN;
            vehicle.DeviceId = dto.DeviceId;

            _dbContext.SaveChanges();
        }
    }
}

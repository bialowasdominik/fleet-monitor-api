using AutoMapper;
using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FleetMonitorAPI.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly FleetMonitorDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DeviceService> _logger; 

        public DeviceService(FleetMonitorDbContext dbContext, IMapper mapper, ILogger<DeviceService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Delete(int id) 
        {
            var device = GetDeviceById(id);

            if (device is null)
                throw new NotFoundException(ExceptionDictionary.DeviceNotFound);

            _dbContext.Devices.Remove(device);
            _dbContext.SaveChanges();  
        }

        public DeviceDto GetById(int id)
        {
            _logger.LogInformation(ExceptionDictionary.DeviceNotFound);

            var device = GetDeviceById(id);

            if (device is null) 
                throw new NotFoundException(ExceptionDictionary.DeviceNotFound);

            var result = _mapper.Map<DeviceDto>(device);
            return result;
        }
        public IEnumerable<DeviceDto> GetAllForList()
        {
            var device = _dbContext.Devices.ToList();
            var result = _mapper.Map<IEnumerable<DeviceDto>>(device);
            return result;
        }

        public PageResult<DeviceDto> GetAll(PaginationQuery query)
        {
            var baseQuery = _dbContext
                .Devices
                .Include(d => d.Positions)
                .Where(d => query.Phrase == null || (d.Name.ToLower().Contains(query.Phrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Device, object>>>() 
                {
                    {nameof(Device.Id), d => d.Id},
                    {nameof(Device.Name), d => d.Name}
                };

                var selectedColumn = columnSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var devices = baseQuery
                .Skip(query.PageSize * (query.PageNumber-1))
                .Take(query.PageSize)
                .ToList();

            var totalItem = baseQuery.Count();
            var deviceDtoList = _mapper.Map<List<DeviceDto>>(devices);
            var result = new PageResult<DeviceDto>(deviceDtoList, totalItem, query.PageSize,query.PageNumber);
            return result;
        }

        public long Create(CreateDeviceDto dto)
        {
            var device = _mapper.Map<Device>(dto);
            _dbContext.Devices.Add(device);
            _dbContext.SaveChanges();

            return device.Id;
        }

        public void Update(UpdateDeviceDto dto, long id) 
        {
            var device = GetDeviceById(id);

            if (device is null)
                throw new NotFoundException(ExceptionDictionary.DeviceNotFound);

            device.Name = dto.Name;
            device.DriverId = dto.DriverId;

            _dbContext.SaveChanges();
        }

        public DeviceDetailsDto GetDetailsById(int id)
        {
            var device = _dbContext
                .Devices
                .Include(d => d.Positions)
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .FirstOrDefault(i => i.Id == id);

            if(device is null)
                throw new NotFoundException(ExceptionDictionary.DeviceNotFound);

            var result = _mapper.Map<DeviceDetailsDto>(device);
            return result;
        }

        public IEnumerable<DevicePositionDto> GetAllWithActualPosition()
        {
            var actualPosition = _dbContext.Devices
                .Include(d => d.Driver)
                .Include(d => d.Vehicle)
                .Include(d => d.Positions
                    .OrderByDescending(p => p.Id)
                    .Take(1))
                .Where(d=>d.Positions
                    .Any())
                .ToList();

            var result = _mapper.Map<List<DevicePositionDto>>(actualPosition);
            return result;
        }

        private Device GetDeviceById(long id) 
        {
            var device = _dbContext
                .Devices
                .Include(d => d.Positions)
                .FirstOrDefault(i => i.Id == id);
           
            return device;
        }

        public long GetAmountOfDevices() 
        {
            var amount = _dbContext.Devices.Count();
            return amount;
        }
    }
}

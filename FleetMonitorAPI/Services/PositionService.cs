using AutoMapper;
using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FleetMonitorAPI.Models.Position;
using FleetMonitorAPI.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace FleetMonitorAPI.Services
{
    public class PositionService : IPositionService
    {
        private readonly FleetMonitorDbContext _dbContext;
        private readonly IMapper _mapper;

        public PositionService(FleetMonitorDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public long Create(int deviceId, CreatePositionDto dto)
        {
            GetDeviceById(deviceId);

            var positionEntity = _mapper.Map<Positions>(dto);

            positionEntity.DeviceId = deviceId;

            _dbContext.Positions.Add(positionEntity);
            _dbContext.SaveChanges();

            return positionEntity.Id;
        }

        public PositionsDto GetById(long deviceId, long positionId)
        {
            var device = GetDeviceById(deviceId);

            var position = _dbContext.Positions.FirstOrDefault(d => d.Id == positionId);
            if(position is null || position.DeviceId != deviceId)
                throw new NotFoundException($"Position not found");

            var positionDto = _mapper.Map<PositionsDto>(position);
            return positionDto;
        }

        public List<PositionsDto> GetAll(long deviceId)
        {
            var device = GetDeviceById(deviceId);
            var result = _mapper.Map<List<PositionsDto>>(device.Positions);
            return result;
        }

        public void RemoveAll(long deviceId)
        {
            var device = GetDeviceById(deviceId);
            _dbContext.RemoveRange(device.Positions);
            _dbContext.SaveChanges();
        }

        public List<PositionsDto> GetByDate(long deviceId, PositionQuery query)
        {
            var device = GetDeviceById(deviceId);
            var result = _mapper.Map<List<PositionsDto>>(device.Positions.Where(p=>p.Time>query.DateFrom && p.Time<query.DateTo));
            return result;
        }

        private Device GetDeviceById(long id) 
        {
            var device = _dbContext
                .Devices
                .Include(d => d.Positions)
                .FirstOrDefault(d => d.Id == id);
            if (device is null)
                throw new NotFoundException(ExceptionDictionary.DeviceNotFound);

            return device;
        }
    }
}

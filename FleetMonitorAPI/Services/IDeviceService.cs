using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Queries;

namespace FleetMonitorAPI.Services
{
    public interface IDeviceService
    {
        long Create(CreateDeviceDto dto);
        PageResult<DeviceDto> GetAll(PaginationQuery query);
        DeviceDto GetById(int id);
        void Delete(int id);
        void Update(UpdateDeviceDto dto, long id);
        DeviceDetailsDto GetDetailsById(int id);
        IEnumerable<DevicePositionDto> GetAllWithActualPosition();
        IEnumerable<DeviceDto> GetAllForList();
        long GetAmountOfDevices();
    }
}
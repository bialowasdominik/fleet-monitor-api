using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Driver;
using FleetMonitorAPI.Models.Queries;

namespace FleetMonitorAPI.Services
{
    public interface IDriverService
    {
        PageResult<DriverDto> GetAll(PaginationQuery query);
        long Create(CreateDriverDto dto);
        void Delete(long id);
        void Update(UpdateDriverDto dto, long id);
        IEnumerable<DriverDto> GetList();
        long GetAmountOfDevices();
    }
}

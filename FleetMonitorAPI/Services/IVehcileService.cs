using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Models.Vehicle;

namespace FleetMonitorAPI.Services
{
    public interface IVehcileService
    {
        IEnumerable<VehicleListDto> GetAll();
        PageResult<VehicleDto> GetAllWithPagination(PaginationQuery query);
        long Create(CreateVehicleDto dto);
        VehicleDetailsDto GetDetailsById(int id);
        void Delete(int id);
        void Update(UpdateVehicleDto dto, long id);
        long GetAmountOfDevices();
    }
}
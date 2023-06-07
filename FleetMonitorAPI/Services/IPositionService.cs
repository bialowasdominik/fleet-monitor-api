using FleetMonitorAPI.Models.Position;
using FleetMonitorAPI.Models.Queries;

namespace FleetMonitorAPI.Services
{
    public interface IPositionService
    {
        long Create(int deviceId, CreatePositionDto dto);
        PositionsDto GetById(long deviceId, long positionId);
        List<PositionsDto> GetAll(long deviceId);
        void RemoveAll(long deviceId);
        List<PositionsDto> GetByDate(long deviceId, PositionQuery query);
    }
}
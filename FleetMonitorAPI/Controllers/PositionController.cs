using FleetMonitorAPI.Models.Position;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetMonitorAPI.Controllers
{
    [Route("api/device/{deviceId}/position")]
    [ApiController]
    [Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromRoute] int deviceId, [FromBody] CreatePositionDto dto) 
        {
            var id = _positionService.Create(deviceId, dto);
            return Created($"api/device/{deviceId}/position/{id}", null);
        }

        [HttpGet("{positionId}")]
        public ActionResult<PositionsDto> Get([FromRoute] long deviceId, [FromRoute] long positionId) 
        {
            PositionsDto position = _positionService.GetById(deviceId, positionId);
            return Ok(position);
        }

        [HttpGet("route")]
        public ActionResult<List<PositionsDto>> GetByDate([FromRoute] long deviceId, [FromQuery] PositionQuery query ) 
        {
            var positions = _positionService.GetByDate(deviceId,query);
            return Ok(positions);
        }

        [HttpGet]
        public ActionResult<List<PositionsDto>> Get([FromRoute] long deviceId) 
        {
            var positions = _positionService.GetAll(deviceId);
            return Ok(positions);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] long deviceId) 
        {
            _positionService.RemoveAll(deviceId);
            return NoContent();
        }
    }
}

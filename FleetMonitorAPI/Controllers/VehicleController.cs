using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Models.Vehicle;
using FleetMonitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetMonitorAPI.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehcileService _vehicleService;

        public VehicleController(IVehcileService vehicleService) 
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<VehicleListDto>> GetAll() 
        {
            var vehicles = _vehicleService.GetAll();
            return Ok(vehicles);
        }

        [HttpGet]
        public ActionResult<PageResult<VehicleDto>> GetAllWithPagination([FromQuery] PaginationQuery query)
        {
            var vehicles = _vehicleService.GetAllWithPagination(query);
            return Ok(vehicles);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateVehicleDto dto) 
        {
            _vehicleService.Create(dto);
            return Created("", null);
        }

        [HttpGet("details/{id}")]
        public ActionResult<VehicleDetailsDto> GetDetailsById(int id) 
        {
            var vehicle = _vehicleService.GetDetailsById(id);
            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            _vehicleService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateVehicleDto dto, [FromRoute]long id) 
        {
            _vehicleService.Update(dto, id);
            return Ok();
        }

        [HttpGet("amount")]
        public ActionResult GetAmountOfDevices()
        {
            var amount = _vehicleService.GetAmountOfDevices();
            return Ok(amount);
        }
    }
}

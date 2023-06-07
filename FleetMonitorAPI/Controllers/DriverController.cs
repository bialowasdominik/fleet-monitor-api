using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Driver;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetMonitorAPI.Controllers
{
    [Route("api/driver")]
    [ApiController]
    [Authorize]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public ActionResult<PageResult<DriverDto>> GetAll([FromQuery] PaginationQuery query) 
        {
            var drivers = _driverService.GetAll(query);
            return Ok(drivers);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateDriverDto dto) 
        {
            var id = _driverService.Create(dto);
            return Created($"/api/driver/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] long id) 
        {
            _driverService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDriverDto dto,[FromRoute] long id) 
        {
            _driverService.Update(dto, id);
            return Ok();
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<DriverDto>> GetList()
        {
            var drivers = _driverService.GetList();
            return Ok(drivers);
        }

        [HttpGet("amount")]
        public ActionResult GetAmountOfDevices()
        {
            var amount = _driverService.GetAmountOfDevices();
            return Ok(amount);
        }
    }
}

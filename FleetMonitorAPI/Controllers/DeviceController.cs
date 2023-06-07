using FleetMonitorAPI.Services;
using FleetMonitorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FleetMonitorAPI.Models.Queries;

namespace FleetMonitorAPI.Controllers
{
    [Route("api/device")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateDeviceDto updateDeviceDto, [FromRoute] long id)
        {
            _deviceService.Update(updateDeviceDto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _deviceService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult CreateDevice([FromBody] CreateDeviceDto dto)
        {
            var id = _deviceService.Create(dto);
            return Created($"/api/device/{id}", null);
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<VehicleDto>> GetAllForList()
        {
            var vehicles = _deviceService.GetAllForList();
            return Ok(vehicles);
        }

        [HttpGet]
        public ActionResult<PageResult<DeviceDto>> GetAll([FromQuery]PaginationQuery query) 
        {
            var devicesDtos = _deviceService.GetAll(query);
            return Ok(devicesDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceDto> GetDeviceById([FromRoute] int id) 
        {
            var devicesDto = _deviceService.GetById(id);
            return Ok(devicesDto);
        }

        [HttpGet("details/{id}")]
        public ActionResult<DeviceDetailsDto> GetDetailsById([FromRoute] int id) 
        {
            var deviceDetailsDto = _deviceService.GetDetailsById(id);
            return Ok(deviceDetailsDto);
        }

        [HttpGet("actual-positions")]
        public ActionResult<IEnumerable<DevicePositionDto>> GetAllWithActualPosition() 
        {
            var devicesDto = _deviceService.GetAllWithActualPosition();
            return Ok(devicesDto);
        }

        [HttpGet("amount")]
        public ActionResult GetAmountOfDevices() 
        {
            var amount = _deviceService.GetAmountOfDevices();
            return Ok(amount);
        }
    }
}

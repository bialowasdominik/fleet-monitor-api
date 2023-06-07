using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Login;
using FleetMonitorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetMonitorAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto) 
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto) 
        {
            LoginResponseDto result = _accountService.LoginUser(dto);
            return Ok(result);
        }
    }
}

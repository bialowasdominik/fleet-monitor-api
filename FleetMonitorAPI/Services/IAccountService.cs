using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Login;

namespace FleetMonitorAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        LoginResponseDto LoginUser(LoginDto dto);
    }
}
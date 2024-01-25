using AUTHSERVICE.Models.Dtos;
using AUTHSERVICE.Models;

namespace AUTHSERVICE.Service.IService
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterRequestDto userDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignUserRoles(string Email, string RoleName);
        Task<ApplicationUser> GetUserById(string Id);
        Task<List<ApplicationUser>> GetUsers();
    }
}

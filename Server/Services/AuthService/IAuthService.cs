using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> GetUserLdapUid(string email);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<string>> Login(UserLoginDto loginDto);
        int GetUserId();
        string GetUserName();
        Task<User> GetUserByUsername(string username);
    }
}

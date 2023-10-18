using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegisterDto request);
        Task<ServiceResponse<string>> Login(UserLoginDto request);
        Task<ServiceResponse<string>> GetUserLdapUid(string email);
        Task<bool> IsUserAuthenticated();
    }
}

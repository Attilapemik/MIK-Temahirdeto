using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<string>> GetUserLdapUid(string email)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<string>>($"api/auth/uid/{email}");
            return result;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/locallogin", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/localregister", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}

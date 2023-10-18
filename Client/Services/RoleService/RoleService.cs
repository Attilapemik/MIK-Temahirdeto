using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _http;

        public RoleService(HttpClient http)
        {
            _http = http;
        }

        public string? ErrorMessage { get; set; } = null;
        public List<Role> Roles { get; set; } = new List<Role>();

        public async Task GetRoles()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Role>>>("api/role");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Roles = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }
    }
}

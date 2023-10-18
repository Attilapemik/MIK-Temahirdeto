using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<Role>>> GetRolesAsync();
    }
}

using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.RoleService
{
    public interface IRoleService
    {
        string? ErrorMessage { get; set; }
        List<Role> Roles { get; set; }
        Task GetRoles();
    }
}

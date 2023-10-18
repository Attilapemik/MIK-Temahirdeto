using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<List<Department>>> GetDepartmentsAsync();
    }
}

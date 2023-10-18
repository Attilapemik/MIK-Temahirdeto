using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.DepartmentService
{
    public interface IDepartmentService
    {
        string? ErrorMessage { get; set; }
        List<Department> Departments { get; set; }
        Task GetDepartments();
    }
}

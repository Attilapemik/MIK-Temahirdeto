using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _http;

        public string? ErrorMessage { get; set; } = null;
        public List<Department> Departments { get ; set ; }

        public DepartmentService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetDepartments()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Department>>>("api/department");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Departments = result.Data;
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

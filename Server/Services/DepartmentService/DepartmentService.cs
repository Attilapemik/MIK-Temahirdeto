using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Department>>> GetDepartmentsAsync()
        {
            var response = new ServiceResponse<List<Department>>
            {
                Data = await _context.Departments!.ToListAsync()
            };

            return response;
        }
    }
}

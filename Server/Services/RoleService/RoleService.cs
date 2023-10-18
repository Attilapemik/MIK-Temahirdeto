using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Role>>> GetRolesAsync()
        {
            var response = new ServiceResponse<List<Role>>
            {
                Data = await _context.Roles!
                    .ToListAsync()
            };
            return response;
        }
    }
}

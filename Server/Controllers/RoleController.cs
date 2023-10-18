using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PannonBlazor.Server.Services.RoleService;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<Role>>>> GetRoles()
        {
            var result = await _roleService.GetRolesAsync();
            return Ok(result);
        }
    }
}

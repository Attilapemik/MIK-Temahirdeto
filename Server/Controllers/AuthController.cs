using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("localregister")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(
                new User
                {
                    Username = request.Username,
                    LdapUid = request.LdapUid,
                    Email = request.Email,
                    NeptunCode = request.NeptunCode.ToUpper()
                },
                request.Password);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("uid/{email}")]
        public async Task<ActionResult<ServiceResponse<int>>> GetUserLdapUid(string email)
        {
            var response = await _authService.GetUserLdapUid(email);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("locallogin")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}

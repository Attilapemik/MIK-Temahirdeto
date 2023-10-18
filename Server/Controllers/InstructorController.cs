using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorService instructorService, IAuthService authService, IHttpContextAccessor http, IMapper mapper)
        {
            _instructorService = instructorService;
            _authService = authService;
            _http = http;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Theme>>> GetInstructor(int id)
        {
            var result = await _instructorService.GetUserByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.Admin},{Roles.HeadOfDepartment}")]
        public async Task<ActionResult<ServiceResponse<List<UserGetDto>>>> GetInstructors()
        {
            
            var response = new ServiceResponse<List<UserGetDto>>();
            
            if (User.IsInRole(Roles.HeadOfDepartment))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var result = await _instructorService.GetInstructorsByDepartmentAsync(user.DepartmentId);

                response = new ServiceResponse<List<UserGetDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<UserGetDto>>(result.Data)
                };
            }
            else if (User.IsInRole(Roles.Admin))
            {
                var result = await _instructorService.GetInstructorsAsync();
                response = new ServiceResponse<List<UserGetDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<UserGetDto>>(result.Data)
                };
            }
            else
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<int>>> EditUser(UserDto request)
        {

            var response = await _instructorService.EditUser(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        /* not implemented, check AuthController for CreateUser(Register)
        [HttpPut("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateUser(UserDto request)
        {

            var response = await _instructorService.CreateUser(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }*/

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _instructorService.DeleteUser(id);

            if(!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}

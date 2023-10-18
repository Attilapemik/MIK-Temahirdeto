using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models.Entity;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;

        public ThemeController(IThemeService themeService, IAuthService authService, IHttpContextAccessor http,
            IMapper mapper)
        {
            _themeService = themeService;
            _authService = authService;
            _http = http;
            _mapper = mapper;
        }

        [HttpGet("{type}/{id:int}")]
        public async Task<ActionResult<object>> GetTheme(string type, int id)
        {
            ServiceResponse<Theme> theme;
            if (type.ToLower().Equals("edit"))
            {
                theme = await _themeService.GetThemeByIdAsync(id);
                var response = new ServiceResponse<ThemeEditDto>
                {
                    Success = theme.Success,
                    Message = theme.Message,
                    Data = _mapper.Map<ThemeEditDto>(theme.Data)
                };
                return Ok(response);
            }
            else if (type.ToLower().Equals("show"))
            {
                theme = await _themeService.GetThemeByIdAsync(id);
                var response = new ServiceResponse<ThemeShowDto>
                {
                    Success = theme.Success,
                    Message = theme.Message,
                    Data = _mapper.Map<ThemeShowDto>(theme.Data)
                };
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        //GET: /api/theme/?semesterId=1
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ThemeDto>>>> GetThemes(int semesterId = 0)
        {
            if (User.IsInRole(Roles.Instructor) || User.IsInRole(Roles.ProgrammeLeader))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var result = await _themeService.GetThemesOfInstructorAsync(username, semesterId);
                var response = new ServiceResponse<List<ThemeDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<ThemeDto>>(result.Data)
                };
                return Ok(response);
            }
            else if (User.IsInRole(Roles.HeadOfDepartment))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var result = await _themeService.GetThemesByDepartment(user.Department!, semesterId);
                var response = new ServiceResponse<List<ThemeDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<ThemeDto>>(result.Data)
                };
                return Ok(response);
            }
            else if (User.IsInRole(Roles.Admin))
            {
                var result = await _themeService.GetThemesAsync(semesterId);
                var response = new ServiceResponse<List<ThemeDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<ThemeDto>>(result.Data)
                };
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("themeprogrammes")]
        public async Task<ActionResult<ServiceResponse<List<ThemeProgrammeDto>>>> GetThemeProgrammes()
        {
            if (User.IsInRole(Roles.Instructor) || User.IsInRole(Roles.ProgrammeLeader))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var result = await _themeService.GetThemeProgrammesByProgramme(user.Programme!, true);
                var response = new ServiceResponse<List<ThemeProgrammeDto>>
                {
                    Success = result.Success,
                    Data = _mapper.Map<List<ThemeProgrammeDto>>(result.Data),
                    Message = result.Message
                };
                return Ok(response);
            }
            else if (User.IsInRole(Roles.HeadOfDepartment))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var result = await _themeService.GetThemeProgrammesByDepartment(user.Department!);
                var response = new ServiceResponse<List<ThemeProgrammeDto>>
                {
                    Success = result.Success,
                    Data = _mapper.Map<List<ThemeProgrammeDto>>(result.Data),
                    Message = result.Message
                };
                return Ok(response);
            }
            else if (User.IsInRole(Roles.Admin))
            {
                var result = await _themeService.GetThemeProgrammes();
                var response = new ServiceResponse<List<ThemeProgrammeDto>>
                {
                    Success = result.Success,
                    Data = _mapper.Map<List<ThemeProgrammeDto>>(result.Data),
                    Message = result.Message
                };
                return Ok(response);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("themes")]
        public async Task<ActionResult<ServiceResponse<List<StudentThemeListDto>>>> GetThemeProgrammesForStudents()
        {
            var result = await _themeService.GetThemeProgrammesForStudents();
            var response = new ServiceResponse<List<StudentThemeListDto>>
            {
                Success = result.Success,
                Message = result.Message,
                Data = _mapper.Map<List<StudentThemeListDto>>(result.Data)
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("showtheme/{id}")]
        public async Task<ActionResult<ServiceResponse<ThemeStudentShowDto>>> GetThemeForStudent(int id)
        {
            var result = await _themeService.GetThemeByIdForStudent(id);

            var response = new ServiceResponse<ThemeStudentShowDto>
            {
                Success = result.Success,
                Message = result.Message,
                Data = _mapper.Map<ThemeStudentShowDto>(result.Data)
            };
            return Ok(response);
        }

        [HttpGet("themeforassignstudent/{themeId}")]
        public async Task<ActionResult<ServiceResponse<ThemeForAssignStudentDto>>> GetThemeForAssignStudent(int themeId)
        {
            var response = await _themeService.GetThemeForAssignStudent(themeId);
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<int>>> EditTheme(ThemeEditDto request)
        {
            if (request.InstructorName.Equals(""))
            {
                Console.WriteLine("No instructor found");
                request.InstructorName = _http.HttpContext!.User!.Identity!.Name!;
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            IEnumerable<Claim> claims = identity.Claims;
            var userId = int.Parse(claims.First(c => c.Type == ClaimTypes.SerialNumber).Value);
            var userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;

            var response = await _themeService.EditTheme(request, userId, userRole);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateTheme(ThemeCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Instructor))
            {
                if (User.IsInRole($"{Roles.HeadOfDepartment},{Roles.Admin}"))
                {
                    request.Instructor = string.Empty;
                }
                else
                {
                    request.Instructor = _http.HttpContext!.User!.Identity!.Name!;
                }
            }

            var response = await _themeService.CreateTheme(request);

            return Ok(response);
        }

        [HttpDelete("delete/{themeId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteTheme(int themeId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            IEnumerable<Claim> claims = identity.Claims;
            var userId = int.Parse(claims.First(c => c.Type == ClaimTypes.SerialNumber).Value);
            var userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;

            var response = await _themeService.DeleteTheme(themeId, userId, userRole);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("activate/{themeId}")]
        public async Task<ActionResult<ServiceResponse<int>>> ActivateTheme(int themeId)
        {
            var response = await _themeService.ActivateTheme(themeId);
            return Ok(response);
        }

        [HttpGet("inactivate/{themeId}")]
        public async Task<ActionResult<ServiceResponse<int>>> InactivateTheme(int themeId)
        {
            var response = await _themeService.InactivateTheme(themeId);
            return Ok(response);
        }

        [HttpPut("approve")]
        public async Task<ActionResult<ServiceResponse<int>>> ApproveTheme(ThemeProgrammeFeedbackDto tpDto)
        {
            var response = await _themeService.ApproveThemeProgramme(tpDto);
            return Ok(response);
        }

        [HttpPut("deny")]
        public async Task<ActionResult<ServiceResponse<int>>> DenyTheme(ThemeProgrammeFeedbackDto tpDto)
        {
            var response = await _themeService.DenyThemeProgramme(tpDto);
            return Ok(response);
        }

        [HttpPut("assignstudent")]
        public async Task<ActionResult<ServiceResponse<bool>>> AssignStudent(AssignStudentDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            IEnumerable<Claim> claims = identity.Claims;
            var userId = int.Parse(claims.First(c => c.Type == ClaimTypes.SerialNumber).Value);
            var userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;
            var response = await _themeService.AssignStudent(request, userId, userRole);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("removestudent")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveStudent(RemoveStudentDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            IEnumerable<Claim> claims = identity.Claims;
            var userId = int.Parse(claims.First(c => c.Type == ClaimTypes.SerialNumber).Value);
            var userRole = claims.First(c => c.Type == ClaimTypes.Role).Value;
            var response = await _themeService.RemoveStudent(request, userId, userRole);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Instructor},{Roles.HeadOfDepartment}")]
        [HttpPost("copytheme")]
        public async Task<ActionResult<ServiceResponse<List<ThemeDto>>>> CopyTheme(CopyThemeDto copyThemeDto)
        {
            if (User.IsInRole(Roles.Instructor))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var response = await _themeService.CopyThemeForInstructor(copyThemeDto, user.Id);
                return Ok(response);
            }
            else if (User.IsInRole(Roles.HeadOfDepartment))
            {
                var username = _http.HttpContext!.User!.Identity!.Name!;
                var user = await _authService.GetUserByUsername(username);
                var response = await _themeService.CopyThemeForHeadOfDepartment(copyThemeDto, user.Department!.Id);
                return Ok(response);
            }
            else if (User.IsInRole(Roles.Admin))
            {
                var response = await _themeService.CopyThemeForAdmin(copyThemeDto);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.HeadOfDepartment}")]
        [HttpGet("multipleneptuncode")]
        public async Task<ActionResult<ServiceResponse<List<StudentMultipleNeptunDto>>>> GetMultipleNeptunCodes()
        {
            var result = await _themeService.GetMultipleNeptunCodes();

            var response = new ServiceResponse<List<StudentMultipleNeptunDto>>
            {
                Success = result.Success,
                Message = result.Message,
                Data = _mapper.Map<List<StudentMultipleNeptunDto>>(result.Data)
            };
            return Ok(response);
        }
    }
}

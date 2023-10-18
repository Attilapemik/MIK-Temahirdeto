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
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        private readonly IMapper _mapper;

        public SemesterController(ISemesterService semesterService, IMapper mapper)
        {
            _semesterService = semesterService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Semester>>> GetSemester(int id)
        {
            if (User.IsInRole(Roles.Instructor) || User.IsInRole(Roles.ProgrammeLeader))
            {
                var result = await _semesterService.GetSemesterForInstructorsById(id);
                var response = new ServiceResponse<SemesterDto>()
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<SemesterDto>(result.Data)
                };
                return Ok(response);
            }
            else
            {
                var result = await _semesterService.GetSemesterById(id);
                return Ok(result);
            }
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<SemesterGetDto>>> GetActiveSemester()
        {
            var result = await _semesterService.GetActiveSemester();
            var response = new ServiceResponse<SemesterGetDto>()
            {
                Success = result.Success,
                Message = result.Message,
                Data = _mapper.Map<SemesterGetDto>(result.Data)
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Semester>>>> GetSemesters()
        {
            if (User.IsInRole(Roles.Instructor) || User.IsInRole(Roles.ProgrammeLeader))
            {
                var result = await _semesterService.GetSemestersForInstructorsAsync();
                var response = new ServiceResponse<List<SemesterGetDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<SemesterGetDto>>(result.Data)
                };
                return Ok(response);
            }
            else
            {
                var result = await _semesterService.GetSemestersAsync();
                var response = new ServiceResponse<List<SemesterDto>>
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = _mapper.Map<List<SemesterDto>>(result.Data)
                };
                return Ok(response);
            }
        }

        [HttpGet("allowinstructor/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> ApproveTheme(int id)
        {
            var response = await _semesterService.AllowInstructor(id);
            return Ok(response);
        }
        [HttpGet("allowstudent/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> AllowStudent(int id)
        {
            var response = await _semesterService.AllowStudent(id);
            return Ok(response);
        }
        [HttpGet("denyinstructor/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DenyInstructor(int id)
        {
            var response = await _semesterService.DenyInstructor(id);
            return Ok(response);
        }
        [HttpGet("denystudent/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> DenyStudent(int id)
        {
            var response = await _semesterService.DenyStudent(id);
            return Ok(response);
        }

        [HttpGet("activate/{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> ActivateSemester(int id)
        {
            var response = await _semesterService.ActivateSemester(id);
            return Ok(response);
        }

        [HttpPut("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateProgramme(SemesterDto request)
        {

            var response = await _semesterService.CreateSemester(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<int>>> EditSemester(SemesterDto request)
        {

            var response = await _semesterService.EditSemester(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteSemester(int id)
        {
            var response = await _semesterService.DeleteSemester(id);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}

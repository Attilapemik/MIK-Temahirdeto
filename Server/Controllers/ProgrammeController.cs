using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgrammeController : ControllerBase
    {
        private readonly IProgrammeService _programmeService;
        private readonly IMapper _mapper;

        public ProgrammeController(IProgrammeService programmeService, IMapper mapper)
        {
            _programmeService = programmeService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Programme>>> GetProgramme(int id)
        {
            var result = await _programmeService.GetProgrammeByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<ProgrammeDto>>>> GetProgrammes()
        {
            var result = await _programmeService.GetProgrammesAsync();
            var response = new ServiceResponse<List<ProgrammeDto>>
            {
                Success = result.Success,
                Message = result.Message,
                Data = _mapper.Map<List<ProgrammeDto>>(result.Data)
            };
            return Ok(response);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<ServiceResponse<int>>> EditProgramme(ProgrammeDto request)
        {

            var response = await _programmeService.EditProgramme(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("create")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateProgramme(ProgrammeCreateDto request)
        {
            var programme = _mapper.Map<Programme>(request);
            var response = await _programmeService.CreateProgramme(programme);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProgramme(int id)
        {
            var response = await _programmeService.DeleteProgramme(id);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}

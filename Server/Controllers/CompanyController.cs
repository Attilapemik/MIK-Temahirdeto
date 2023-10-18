using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PannonBlazor.Server.Services.CompanyService;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _CompanyService;

        public CompanyController(ICompanyService CompanyService)
        {
            _CompanyService = CompanyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Company>>> GetCompany(int id)
        {
            var result = await _CompanyService.GetCompanyByIdAsync(id);
            return Ok(result);
        }

        //GET: /api/Company/?enabled=false
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Company>>>> GetCompanies(bool enabled = true)
        {
            var result = await _CompanyService.GetCompaniesAsync(enabled);
            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> EditCompany(CompanyDto request)
        {

            var response = await _CompanyService.EditCompany(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateCompany(CompanyDto request)
        {

            var response = await _CompanyService.CreateCompany(request);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteCompany(int id)
        {
            var response = await _CompanyService.DeleteCompany(id);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        // POST: /api/Company/restore/{id}
        [HttpPost("restore/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<int>>> RestoreCompany(int id)
        {
            var response = await _CompanyService.RestoreCompany(id);

            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }
    }
}

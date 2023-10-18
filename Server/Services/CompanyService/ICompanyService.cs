using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<ServiceResponse<Company>> GetCompanyByIdAsync(int id);
        Task<ServiceResponse<List<Company>>> GetCompaniesAsync(bool enabled = true);
        Task<ServiceResponse<int>> EditCompany(CompanyDto companyDto);
        Task<ServiceResponse<int>> CreateCompany(CompanyDto companyDto);
        Task<ServiceResponse<int>> DeleteCompany(int id);
        Task<ServiceResponse<int>> RestoreCompany(int id);
    }
}

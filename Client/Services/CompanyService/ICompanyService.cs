using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.CompanyService
{
    public interface ICompanyService
    {
        string? ErrorMessage { get; set; }
        Company Company { get; set; }
        List<Company> Companies { get; set; }

        Task GetCompany(string? id);
        Task GetCompanies(bool enabled = true);

        Task CreateCompany(CompanyDto request);
        Task EditCompany(CompanyDto request);
        Task DeleteCompany(int id);
        Task RestoreCompany(int id);
    }
}

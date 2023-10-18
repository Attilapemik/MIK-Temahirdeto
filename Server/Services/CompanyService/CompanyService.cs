using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;
        
        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Company>> GetCompanyByIdAsync(int id)
        {
            if (!await CompanyExists(id))
            {
                Console.WriteLine("Company not exists");
                return new ServiceResponse<Company>
                {
                    Success = false,
                    Message = "Nem létezik ilyen cég",
                    Data = null
                };
            }
            var response = new ServiceResponse<Company>
            {
                Data = await _context.Companies!.Where(p => p.Id == id && p.Enabled).FirstOrDefaultAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Company>>> GetCompaniesAsync(bool enabled = true)
        {
            var response = new ServiceResponse<List<Company>>
            {
                Data = await _context.Companies!.Where(x=>x.Enabled == enabled).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<int>> CreateCompany(CompanyDto companyDto)
        {
            if (await CompanyExists(companyDto.Id))
            {
                Console.WriteLine("Company already exists");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Már van ilyen cég"
                };
            }

            var Company = new Company { Name = companyDto.Name };

            _context.Companies!.Add(Company);
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<int> { Data = Company.Id, Success = true, Message = "Cég létrehozva!" };

            return response;
        }

        public async Task<ServiceResponse<int>> EditCompany(CompanyDto companyDto)
        {
            if (!await CompanyExists(companyDto.Id))
            {
                Console.WriteLine("Company not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nem található a cég"
                };
            }

            var Company = await _context.Companies!.FindAsync(companyDto.Id);
            Company!.Name = companyDto.Name;

            _context.Companies.Update(Company);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = Company.Id, Success = true, Message = "Cég módosítva" };
        }

        public async Task<ServiceResponse<int>> DeleteCompany(int id)
        {
            if (!await CompanyExists(id))
            {
                Console.WriteLine("Company not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nem található a cég"
                };
            }

            var Company = await _context.Companies!.FindAsync(id);
            Company.Enabled = false;

            _context.Companies.Update(Company);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = id, Success = true, Message = "Cég törölve" };
        }

        public async Task<ServiceResponse<int>> RestoreCompany(int id)
        {
            if (!await CompanyExists(id, false))
            {
                Console.WriteLine("Company not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nem található a cég"
                };
            }

            var Company = await _context.Companies!.FindAsync(id);
            Company.Enabled = true;

            _context.Companies.Update(Company);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = id, Success = true, Message = "Cég visszaállítva" };
        }

        private async Task<bool> CompanyExists(int id, bool enabled = true)
        {
            if (await _context.Companies!.AnyAsync(p => p.Id == id && p.Enabled == enabled)) return true;
            return false;
        }
    }
}

using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;
using Syncfusion.Blazor;

namespace PannonBlazor.Client.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _http;

        public CompanyService(HttpClient http)
        {
            _http = http;
        }

        public string? ErrorMessage { get; set; } = null;
        public List<Company> Companies { get; set; } = new List<Company>();
        public Company Company { get; set; }

        public async Task GetCompany(string? id = "0")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = "0";
            }
            var result = await _http.GetFromJsonAsync<ServiceResponse<Company>>("api/Company/" + id.ToString());
            if (result != null)
            {
                if (result.Data != null)
                {
                    Company = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetCompanies(bool enabled = true)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Company>>>($"api/Company/{(enabled ? "" : "?enabled=false")}");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Companies = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task CreateCompany(CompanyDto request)
        {
            var result = await _http.PutAsJsonAsync("api/Company/create", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            if (response != null)
            {
                if (!response.Success)
                {
                    ErrorMessage = response.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task DeleteCompany(int id)
        {
            var result = await _http.DeleteAsync($"api/Company/delete/{id}");
            var response =  await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            if (response != null)
            {
                if (!response.Success)
                {
                    ErrorMessage = response.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task EditCompany(CompanyDto request)
        {
            var result = await _http.PutAsJsonAsync("api/Company/edit", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            if (response != null)
            {
                if (!response.Success)
                {
                    ErrorMessage = response.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task RestoreCompany(int id)
        {
            var result = await _http.PostAsync($"api/Company/restore/{id}", null);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            if (response != null)
            {
                if (!response.Success)
                {
                    ErrorMessage = response.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }
    }
}

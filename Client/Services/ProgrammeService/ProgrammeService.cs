using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.ProgrammeService
{
    public class ProgrammeService : IProgrammeService
    {
        private readonly HttpClient _http;

        public ProgrammeService(HttpClient http)
        {
            _http = http;
        }

        public string? ErrorMessage { get; set; } = null;
        public List<ProgrammeDto> Programmes { get; set; } = new List<ProgrammeDto>();
        public ProgrammeDto Programme { get ; set ; }

        public async Task CreateProgramme(ProgrammeCreateDto request)
        {
            var result = await _http.PutAsJsonAsync("api/programme/create", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            if (response != null && result.IsSuccessStatusCode)
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

        public async Task DeleteProgramme(string id)
        {
            var result = await _http.DeleteAsync("api/programme/delete/" + id);
            if (result != null)
            {
                if (!result.IsSuccessStatusCode)
                {
                    ErrorMessage = $"Hiba lépett fel a törlés során. Állapotkód: {result.StatusCode}";
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task EditProgramme(ProgrammeDto request)
        {
            var result = await _http.PutAsJsonAsync("api/programme/edit", request);
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

        public async Task GetProgramme(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProgrammeDto>>("api/programme/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    Programme = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetProgrammes()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ProgrammeDto>>>("api/programme");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Programmes = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }
    }
}

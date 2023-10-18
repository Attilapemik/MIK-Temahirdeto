using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly HttpClient _http;

        public InstructorService(HttpClient http)
        {
            _http = http;
        }

        public string? ErrorMessage { get; set; } = null;
        public List<UserGetDto> Instructors { get; set; } = new List<UserGetDto>();
        public User User { get ; set ; }

        public async Task CreateUser(UserDto request)
        {
            var result = await _http.PutAsJsonAsync("api/instructor/create", request);
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

        public async Task DeleteUser(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/instructor/delete/" + id);
            if (result != null)
            {
                if (!result.Success)
                {
                    ErrorMessage = result.Message;
                }
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task EditUser(UserDto request)
        {
            var result = await _http.PutAsJsonAsync("api/instructor/edit", request);
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

        public async Task GetInstructor(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<User>>("api/instructor/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    User = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetInstructors()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<UserGetDto>>>("api/instructor");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Instructors = result.Data;
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

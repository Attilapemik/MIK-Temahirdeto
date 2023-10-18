using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.SemesterService
{
    public class SemesterService<T> : ISemesterService<T> where T : class
    {
        private readonly HttpClient _http;

        public SemesterService(HttpClient http)
        {
            _http = http;
        }

        public string? ErrorMessage { get; set; } = null;
        public T Semester { get; set; }
        public List<T> Semesters { get; set; } = new List<T>();

        public async Task ActivateSemester(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/activate/" + id);
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

        public async Task AllowInstructor(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/allowinstructor/" + id);
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

        public async Task AllowStudent(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/allowstudent/" + id);
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

        public async Task CreateSemester(SemesterDto request)
        {
            var result = await _http.PutAsJsonAsync("api/semester/create", request);
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

        public async Task DeleteSemester(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/delete/" + id);
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

        public async Task DenyInstructor(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/denyinstructor/" + id);
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

        public async Task DenyStudent(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/semester/denystudent/" + id);
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

        public async Task EditSemester(SemesterDto request)
        {
            var result = await _http.PutAsJsonAsync("api/semester/edit", request);
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

        public async Task GetActiveSemester()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<T?>>("api/semester/active");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Semester = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetSemesterById(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<T>>("api/semester/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    Semester = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetSemesters()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<T>>>("api/semester");
            if (result != null)
            {
                if (result.Data != null)
                {
                    Semesters = result.Data;
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

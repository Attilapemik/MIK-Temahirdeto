using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.ThemeService
{
    public class ThemeService : IThemeService
    {
        private readonly HttpClient _http;
        public List<ThemeDto> Themes { get; set; } = new List<ThemeDto>();
        public Theme Theme { get; set; } = new Theme();
        public ThemeStudentShowDto ThemeStudentShow { get; set; } = new ThemeStudentShowDto();
        public List<ThemeProgrammeDto> ThemeProgrammes { get; set; } = new List<ThemeProgrammeDto>();
        public List<StudentMultipleNeptunDto> StudentMultipleNeptunCodes { get; set; } = new List<StudentMultipleNeptunDto>();
        public List<StudentThemeListDto> StudentThemes { get; set; } = new List<StudentThemeListDto>();
        public string? ErrorMessage { get; set;} = null;

        public ThemeService(HttpClient http)
        {
            _http = http;            
        }
        
        public async Task<ThemeShowDto?> GetThemeShow(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ThemeShowDto>>("api/theme/show/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    return result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
            return null;
        }

        public async Task<ThemeEditDto?> GetThemeEdit(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ThemeEditDto>>("api/theme/edit/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    return result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
            return null;
        }

        public async Task GetThemes(string? semesterId = null)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ThemeDto>>>("api/theme/" + (semesterId == null ? "" : "?semesterId=" + semesterId.ToString()));
            if (result != null)
            {
                if (result.Data != null)
                {
                    Themes = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task<ServiceResponse<int>> EditTheme(ThemeEditDto request)
        {
            var result = await _http.PutAsJsonAsync("api/theme/edit", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();

        }

        public async Task<ServiceResponse<int>> CreateTheme(ThemeCreateDto request)
        {
            var result = await _http.PutAsJsonAsync("api/theme/create", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<bool>> DeleteTheme(int themeId)
        {
            var result = await _http.DeleteAsync("api/theme/delete/" + themeId);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task ActivateTheme(int themeId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/theme/activate/" + themeId);
            if (result != null)
            {
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task InactivateTheme(int themeId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/theme/inactivate/" + themeId);
            if (result != null)
            {
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task<ServiceResponse<int>> ApproveThemeProgramme(ThemeProgrammeFeedbackDto tpDto)
        {
            var result = await _http.PutAsJsonAsync<ThemeProgrammeFeedbackDto>("api/theme/approve/", tpDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
        public async Task<ServiceResponse<int>> DenyThemeProgramme(ThemeProgrammeFeedbackDto tpDto)
        {
            var result = await _http.PutAsJsonAsync<ThemeProgrammeFeedbackDto>("api/theme/deny/", tpDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task GetThemeProgrammes()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ThemeProgrammeDto>>>("api/theme/themeprogrammes");
            if (result != null)
            {
                if (result.Data != null)
                {
                    ThemeProgrammes = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetMultipleNeptunCodes()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<StudentMultipleNeptunDto>>>("api/theme/multipleneptuncode");
            if (result != null)
            {
                if (result.Data != null)
                {
                    StudentMultipleNeptunCodes = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetThemeProgrammesForStudents()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<StudentThemeListDto>>>("api/theme/themes");
            if (result != null)
            {
                if (result.Data != null)
                {
                    StudentThemes = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task GetThemeForStudent(string id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ThemeStudentShowDto>>("api/theme/showtheme/" + id);
            if (result != null)
            {
                if (result.Data != null)
                {
                    ThemeStudentShow = result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
        }

        public async Task<ThemeForAssignStudentDto?> GetThemeForAssignStudent(string themeId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<ThemeForAssignStudentDto>>("api/theme/themeforassignstudent/" + themeId);
            if (result != null)
            {
                if (result.Data != null)
                {
                    return result.Data;
                }
                ErrorMessage = result.Message;
            }
            else
            {
                ErrorMessage = Messages.RequestFailed;
            }
            return null;
        }

        public async Task<ServiceResponse<bool>> AssignStudent(AssignStudentDto assignStudentDto)
        {
            var result = await _http.PutAsJsonAsync<AssignStudentDto>("api/theme/assignstudent/", assignStudentDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<bool>> RemoveStudent(RemoveStudentDto removeStudentDto)
        {
            var result = await _http.PutAsJsonAsync<RemoveStudentDto>("api/theme/removestudent/", removeStudentDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<bool>> CopyTheme(CopyThemeDto copyThemeDto)
        {
            var result = await _http.PostAsJsonAsync<CopyThemeDto>("api/theme/copytheme/", copyThemeDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}

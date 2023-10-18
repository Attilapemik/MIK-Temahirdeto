using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.ThemeService
{
    public interface IThemeService
    {
        Theme Theme { get; set; }
        ThemeStudentShowDto ThemeStudentShow { get; set; }
        List<ThemeDto> Themes { get; set; }
        List<ThemeProgrammeDto> ThemeProgrammes { get; set; }
        List<StudentMultipleNeptunDto> StudentMultipleNeptunCodes { get; set; }
        List<StudentThemeListDto> StudentThemes { get; set; }
        string? ErrorMessage { get; set; }
        Task<ThemeShowDto?> GetThemeShow(string id);
        Task<ThemeEditDto?> GetThemeEdit(string id);
        Task GetThemes(string? semesterId = null);
        Task GetThemeProgrammes();
        Task GetMultipleNeptunCodes();
        Task<ServiceResponse<int>> CreateTheme(ThemeCreateDto request);
        Task<ServiceResponse<int>> EditTheme(ThemeEditDto request);
        Task<ServiceResponse<bool>> DeleteTheme(int themeId);

        Task ActivateTheme(int id);
        Task InactivateTheme(int id);
        Task<ServiceResponse<int>> ApproveThemeProgramme(ThemeProgrammeFeedbackDto tpDtop);
        Task<ServiceResponse<int>> DenyThemeProgramme(ThemeProgrammeFeedbackDto tpDto);

        Task GetThemeProgrammesForStudents();
        Task GetThemeForStudent(string id);

        Task<ThemeForAssignStudentDto?> GetThemeForAssignStudent(string themeId);
        Task<ServiceResponse<bool>> AssignStudent(AssignStudentDto assignStudentDto);
        Task<ServiceResponse<bool>> RemoveStudent(RemoveStudentDto removeStudentDto);

        Task<ServiceResponse<bool>> CopyTheme(CopyThemeDto copyThemeDto);
    }
}

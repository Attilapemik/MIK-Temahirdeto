using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.ThemeService
{
    public interface IThemeService
    {
        Task<ServiceResponse<Theme>> GetThemeByIdAsync(int id);
        Task<ServiceResponse<List<Theme>>> GetThemesAsync(int semesterId = 0);
        Task<ServiceResponse<List<Theme>>> GetThemesByProgrammeAsync(Programme programme);
        Task<ServiceResponse<List<Theme>>> GetUnapprovedThemesAsync();
        Task<ServiceResponse<List<Theme>>> GetThemesOfInstructorAsync(string name, int semesterId = 0);
        Task<ServiceResponse<List<Theme>>> GetThemesByDepartment(Department department, int semesterId = 0);
        Task<ServiceResponse<List<Student>>> GetMultipleNeptunCodes();
        Task<ServiceResponse<int>> EditTheme(ThemeEditDto editThemeDto, int userId, string userRole);
        Task<ServiceResponse<int>> CreateTheme(ThemeCreateDto themeDto);
        Task<ServiceResponse<bool>> DeleteTheme(int themeId, int userId, string userRole);
        Task<ServiceResponse<int>> ActivateTheme(int id);
        Task<ServiceResponse<int>> InactivateTheme(int id);
        Task<ServiceResponse<int>> ApproveThemeProgramme(ThemeProgrammeFeedbackDto tpDto);
        Task<ServiceResponse<int>> DenyThemeProgramme(ThemeProgrammeFeedbackDto tpDto);

        Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammes();
        Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammesByProgramme(Programme programme, bool onlyUnderApproval = false);
        Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammesByDepartment(Department department);

        Task<ServiceResponse<List<Theme>>> GetThemeProgrammesForStudents();
        Task<ServiceResponse<Theme>> GetThemeByIdForStudent(int id);

        Task<ServiceResponse<ThemeForAssignStudentDto>> GetThemeForAssignStudent(int themeId);
        Task<ServiceResponse<bool>> AssignStudent(AssignStudentDto assignStudentDto, int userId, string userRole);
        Task<ServiceResponse<bool>> RemoveStudent(RemoveStudentDto removeStudentDto, int userId, string userRole);

        Task<ServiceResponse<bool>> CopyThemeForAdmin(CopyThemeDto copyThemeDto);
        Task<ServiceResponse<bool>> CopyThemeForInstructor(CopyThemeDto copyThemeDto, int instructorId);
        Task<ServiceResponse<bool>> CopyThemeForHeadOfDepartment(CopyThemeDto copyThemeDto, int departmentId);
    }
}

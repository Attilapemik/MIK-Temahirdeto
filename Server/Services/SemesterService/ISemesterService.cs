using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.SemesterService
{
    public interface ISemesterService
    {
        Task<ServiceResponse<Semester>> GetSemesterById(int id);
        Task<ServiceResponse<Semester>> GetSemesterForInstructorsById(int id);
        Task<ServiceResponse<Semester>> GetActiveSemester();
        Task<ServiceResponse<List<Semester>>> GetSemestersAsync();
        Task<ServiceResponse<List<Semester>>> GetSemestersForInstructorsAsync();

        Task<ServiceResponse<int>> AllowInstructor(int id);
        Task<ServiceResponse<int>> AllowStudent(int id);
        Task<ServiceResponse<int>> DenyInstructor(int id);
        Task<ServiceResponse<int>> DenyStudent(int id);
        Task<ServiceResponse<int>> ActivateSemester(int id);

        Task<ServiceResponse<int>> CreateSemester(SemesterDto semester);
        Task<ServiceResponse<int>> EditSemester(SemesterDto semester);
        Task<ServiceResponse<int>> DeleteSemester(int id);
    }
}
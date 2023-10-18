using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.SemesterService
{
    public interface ISemesterService<T> where T : class
    {
        string? ErrorMessage { get; set; }
        T Semester { get; set; }
        List<T> Semesters { get; set; }
        Task GetActiveSemester();
        Task GetSemesterById(string id);
        Task GetSemesters();

        Task AllowInstructor(int id);
        Task DenyInstructor(int id);
        Task AllowStudent(int id);
        Task DenyStudent(int id);
        Task ActivateSemester(int id);

        Task CreateSemester(SemesterDto request);
        Task EditSemester(SemesterDto request);
        Task DeleteSemester(string id);
    }
}

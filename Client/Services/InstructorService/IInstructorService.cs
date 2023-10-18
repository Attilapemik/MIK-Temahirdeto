using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Client.Services.InstructorService
{
    public interface IInstructorService
    {
        string? ErrorMessage { get; set; }
        User User { get; set; }
        List<UserGetDto> Instructors { get; set; }
        Task GetInstructors();
        Task GetInstructor(string id);
        Task CreateUser(UserDto request);
        Task EditUser(UserDto request);
        Task DeleteUser(string id);
    }
}

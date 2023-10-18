using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.InstructorService
{
    public interface IInstructorService
    {
        Task<ServiceResponse<User>> GetUserByIdAsync(int id);
        Task<ServiceResponse<List<User>>> GetInstructorsAsync();
        Task<ServiceResponse<List<User>>> GetInstructorsByDepartmentAsync(int? departmentId);
        Task<ServiceResponse<int>> EditUser(UserDto themeDto);
        Task<ServiceResponse<int>> CreateUser(UserDto themeDto);
        Task<ServiceResponse<int>> DeleteUser(int id);
    }
}

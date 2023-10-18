using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.ProgrammeService
{
    public interface IProgrammeService
    {
        Task<ServiceResponse<Programme>> GetProgrammeByIdAsync(int id);
        Task<ServiceResponse<List<Programme>>> GetProgrammesAsync();
        Task<ServiceResponse<int>> EditProgramme(ProgrammeDto programmeDto);
        Task<ServiceResponse<int>> CreateProgramme(Programme programme);
        Task<ServiceResponse<int>> DeleteProgramme(int id);
    }
}

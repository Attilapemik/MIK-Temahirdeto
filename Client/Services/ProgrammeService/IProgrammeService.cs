using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Client.Services.ProgrammeService
{
    public interface IProgrammeService
    {
        string? ErrorMessage { get; set; }
        ProgrammeDto Programme { get; set; }
        List<ProgrammeDto> Programmes { get; set; }

        Task GetProgramme(string id);
        Task GetProgrammes();

        Task CreateProgramme(ProgrammeCreateDto request);
        Task EditProgramme(ProgrammeDto request);
        Task DeleteProgramme(string id);
    }
}

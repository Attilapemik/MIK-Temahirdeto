using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.ProgrammeService
{
    public class ProgrammeService : IProgrammeService
    {
        private readonly DataContext _context;

        public ProgrammeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<int>> CreateProgramme(Programme programme)
        {
            _context.Programmes!.Add(programme);
            await _context.SaveChangesAsync();

            var response = new ServiceResponse<int> { Data = programme.Id, Success = true, Message = "Szak létrehozva!" };

            return response;
        }

        public async Task<ServiceResponse<int>> DeleteProgramme(int id)
        {
            if (!await ProgrammeExists(id))
            {
                Console.WriteLine("Programme not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen szak"
                };
            }

            var programme = await _context.Programmes!.FindAsync(id);
            programme!.IsDeleted = true;

            _context.Programmes.Update(programme!);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = id, Success = true, Message = "Szak törölve" };
        }

        public async Task<ServiceResponse<int>> EditProgramme(ProgrammeDto programmeDto)
        {
            var programme = await _context.Programmes!.FirstOrDefaultAsync(p => p.Id == programmeDto.Id && !p.IsDeleted);
            if (programme == null)
            {
                Console.WriteLine("Programme not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen szak"
                };
            }

            programme!.Name = programmeDto.Name;
            programme!.Color = programmeDto.Color;
            programme!.ProgrammeType = programmeDto.ProgrammeType;

            _context.Programmes.Update(programme!);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = programme.Id, Success = true, Message = "Szak módosítva" };
        }

        public async Task<ServiceResponse<Programme>> GetProgrammeByIdAsync(int id)
        {
            var programme = await _context.Programmes!.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (programme == null)
            {
                Console.WriteLine("Programme not found");
                return new ServiceResponse<Programme>
                {
                    Success = false,
                    Message = "Nincs ilyen szak"
                };
            }

            var response = new ServiceResponse<Programme>
            {
                Data = programme
            };
            return response;
        }

        public async Task<ServiceResponse<List<Programme>>> GetProgrammesAsync()
        {
            var response = new ServiceResponse<List<Programme>>
            {
                Data = await _context.Programmes!.Where(p => !p.IsDeleted).ToListAsync()
            };

            return response;
        }

        public async Task<bool> ProgrammeExists(int id)
        {
            if (await _context.Programmes!.AnyAsync(p => p.Id == id && !p.IsDeleted)) return true;
            return false;
        }
    }
}

using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly DataContext _context;

        public InstructorService(DataContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<int>> CreateUser(UserDto themeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> DeleteUser(int id)
        {
            if(!await UserExists(id))
            {
                Console.WriteLine("User not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = Messages.UserNotExists
                };
            }

            var user = await _context.Users!.FindAsync(id);

            _context.Users.Remove(user!);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = id};
        }

        public async Task<ServiceResponse<int>> EditUser(UserDto userDto)
        {
            if (!ThemeService.ThemeService.IsNeptunCode(userDto.NeptunCode) && !string.IsNullOrWhiteSpace(userDto.NeptunCode))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = Messages.WrongNeptunCode
                };
            }

            if (!await UserExists(userDto.Id))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = Messages.UserNotExists
                };
            }

            var user = await _context.Users!.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userDto.Id);

            user!.Username = userDto.Username;
            user.Email = userDto.Email;
            user.DepartmentId = userDto.DepartmentId;
            user.ProgrammeId = userDto.ProgrammeId;
            user.NeptunCode = userDto.NeptunCode.ToUpper();
            user.Roles = _context.Roles!.Where(r => userDto.RoleIds.Contains(r.Id.ToString())).ToList();
            user.LdapUid = userDto.LdapUid;

            _context.Users!.Update(user);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = user.Id};
        }

        public async Task<ServiceResponse<List<User>>> GetInstructorsAsync()
        {
            var response = new ServiceResponse<List<User>>
            {
                Data = await _context.Users!
                    .Include(u => u.Roles)
                    .Include(u => u.Department)
                    .Include(u => u.Programme)
                    .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<User>>> GetInstructorsByDepartmentAsync(int? departmentId)
        {
            var response = new ServiceResponse<List<User>>
            {
                Data = await _context.Users!.Where(i => i.DepartmentId == departmentId).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByIdAsync(int id)
        {
            var user = await _context.Users!.Where(u => u.Id == id)
                .Include(i => i.Programme)
                .Include(i => i.Department)
                .Include(i => i.Roles)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "Felhasználó nem található"
                };
            }

            var response = new ServiceResponse<User>
            {
                Data = user
            };
            return response;
        }

        public async Task<bool> UserExists(int id)
        {
            if (await _context.Users!.AnyAsync(u => u.Id == id)) return true;
            return false;
        }
    }
}

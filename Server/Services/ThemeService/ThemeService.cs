using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;
using Syncfusion.Blazor.Data;
using System.Text.RegularExpressions;

namespace PannonBlazor.Server.Services.ThemeService
{
    public class ThemeService : IThemeService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public ThemeService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<List<Theme>>> GetThemesAsync(int semesterId = 0)
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => semesterId == 0 ? s.IsActive == true : s.Id == semesterId);
            if (semester == null)
            {
                Console.WriteLine("Semesters not Found");
                return new ServiceResponse<List<Theme>>
                {
                    Success = false,
                    Data = null,
                    Message = "Jelenleg nincs témahirdetési időszak"
                };
            }

            var themes = await _context.ThemeProgrammes!.Where(tp => tp.SemesterId == semester!.Id)
                .Include(tp => tp!.Theme!.Instructor)
                .Include(tp => tp!.Theme!.ExternalCompany)
                .Include(tp => tp!.Theme!.Status)
                .Include(tp => tp!.Theme!.ThemeProgrammes!).ThenInclude(tp => tp.Semester)
                .Select(tp => tp.Theme)
                .Distinct()
                .ToListAsync();

            var response = new ServiceResponse<List<Theme>>
            {
                Message = themes.Count == 0 ? "Jelenleg nincsenek témák meghirdetve" : null,
                Data = themes
            };

            return response;
        }

        public async Task<ServiceResponse<List<Theme>>> GetThemesByProgrammeAsync(Programme programme)
        {
            var themes = await _context.ThemeProgrammes!.Where(tp => tp.ProgrammeId == programme.Id && tp.Approval!.Id == 1)
                .Include(p => p!.Theme!.Instructor)
                .Include(p => p!.Theme!.Status!).Include(tp => tp!.Theme!.ThemeProgrammes!).ThenInclude(tp => tp.Semester)
                .Select(tp => tp.Theme)
                .Distinct()
                .ToListAsync();

            var response = new ServiceResponse<List<Theme>>
            {
                Data = themes
            };

            return response;
        }

        public async Task<ServiceResponse<List<Theme>>> GetUnapprovedThemesAsync()
        {
            var themes = await _context.ThemeProgrammes!.Where(tp => tp.Approval!.Id == 1).Select(tp => tp.Theme).ToListAsync();
            var response = new ServiceResponse<List<Theme>>
            {
                Data = themes
            };
            return response;
        }

        public async Task<ServiceResponse<List<Theme>>> GetThemesOfInstructorAsync(string username, int semesterId = 0)
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsVisibleInstructor == true && semesterId == 0 ? s.IsActive == true : s.Id == semesterId);
            if (semester == null)
            {
                return new ServiceResponse<List<Theme>>
                {
                    Success = false,
                    Data = null,
                    Message = semesterId == 0 ? "Jelenleg nincs témahirdetési időszak!" : "Nincs ilyen félév!"
                };
            }
            var themes = await _context.ThemeProgrammes!
                .Include(tp => tp!.Theme!.Instructor)
                .Include(tp => tp!.Theme!.Status)
                .Include(tp => tp!.Theme!.ThemeProgrammes!).ThenInclude(tp => tp.Semester)
                .Where(tp => tp.SemesterId == semester!.Id && tp.Theme!.Instructor!.Username.Equals(username))
                .Select(tp => tp.Theme)
                .Distinct()
                .ToListAsync();

            var response = new ServiceResponse<List<Theme>>
            {
                Data = themes
            };

            return response;
        }

        public async Task<ServiceResponse<List<Theme>>> GetThemesByDepartment(Department department, int semesterId = 0)
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => semesterId == 0 ? s.IsActive == true : s.Id == semesterId);
            if (semester == null)
            {
                return new ServiceResponse<List<Theme>>
                {
                    Success = false,
                    Data = null,
                    Message = semesterId == 0 ? "Jelenleg nincs témahirdetési időszak!" : "Nincs ilyen félév!"
                };
            }
            var themes = await _context.ThemeProgrammes!
                .Include(tp => tp.Theme!.Instructor)
                .Include(tp => tp.Theme!.Status)
                .Include(tp => tp.Theme!.ThemeProgrammes!).ThenInclude(tp => tp.Semester)
                .Where(tp => tp.SemesterId == semester!.Id && tp.Theme!.Instructor!.DepartmentId == department.Id)
                .Select(tp => tp.Theme)
                .Distinct()
                .ToListAsync();
            var response = new ServiceResponse<List<Theme>>
            {
                Data = themes
            };

            return response;
        }

        public async Task<ServiceResponse<Theme>> GetThemeByIdAsync(int id)
        {
            var theme = await _context.Themes!.Where(t => t!.Id == id)
                .Include(t => t!.Instructor!).ThenInclude(i => i.Programme!).ThenInclude(p => p.ThemeProgrammes)
                .Include(t => t!.Status)
                .Include(t => t!.ExternalCompany)
                .Include(t => t!.StudentProgramme)
                .Include(t => t!.ThemeProgrammes!).ThenInclude(tp => tp.Approval)
                .Include(t => t!.ThemeProgrammes!).ThenInclude(tp => tp.Programme)
                .Include(t => t!.Students)
                .FirstAsync();

            var response = new ServiceResponse<Theme>
            {
                Data = theme
            };
            return response;
        }

        public async Task<ServiceResponse<List<Student>>> GetMultipleNeptunCodes()
        {
            var students = await _context.Students!
            .Include(s => s.Programme)
            .Include(s => s.Semester)
            .Include(s => s.Theme).ThenInclude(t => t.Instructor)
            .Where(s => _context.Students!.Count(s2 => s2.NeptunCode == s.NeptunCode) > 1)
            .ToListAsync();

            var response = new ServiceResponse<List<Student>>
            {
                Data = students
            };

            return response;
        }

        public async Task<ServiceResponse<int>> EditTheme(ThemeEditDto editThemeDto, int userId, string userRole)
        {
            if (editThemeDto.SelectedProgrammes.Count == 0)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Legalább egy szakon meg kell hirdetni a témát"
                };
            }

            if (!await ThemeExists(editThemeDto.Id))
            {
                Console.WriteLine("Theme not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen téma"
                };
            }

            string? checkProgrammes = await CheckProgrammes(editThemeDto.SelectedProgrammes);
            if (checkProgrammes != null)
            {
                return new ServiceResponse<int> { Success = false, Message = checkProgrammes };
            }
            
            var theme = await _context.Themes!.FindAsync(editThemeDto.Id);
            var oldTheme = new Theme(theme, theme.Id);

            if (!string.IsNullOrEmpty(theme.StudentCode))
            {
                return new ServiceResponse<int> { Success = false, Message = "A téma nem módosítható, mert van már hallgató hozzárendelve!" };
            }

            var instructor = _context.Users!.FirstOrDefault(u => u.Username.Equals(editThemeDto.InstructorName));
            if (instructor == null)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Témavezetőt meg kell adni!"
                };
            }

            if (userRole == Roles.Instructor && theme.InstructorId != userId)
            {
                return new ServiceResponse<int> { Success = false, Message = "Az oktató csak a saját témáját tudja módosítani!" };
            }
            else if (userRole == Roles.HeadOfDepartment)
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userId);

                if (user!.DepartmentId != instructor!.DepartmentId)
                {
                    return new ServiceResponse<int> { Success = false, Message = "Tanszéki adminisztrátor csak a saját tanszékéhez tartozó témát tudja módosítani!" };
                }
            }

            theme!.Title = editThemeDto.Title;
            theme.Description = editThemeDto.Description;
            theme.Instructor = instructor;
            theme.InstructorId = instructor.Id;
            theme.Status = new Status { Id = 1, Name = "Aktív" };
            theme.StatusId = 1;
            theme.IsExternal = editThemeDto.IsExternal;
            theme.IsDual = editThemeDto.IsDual;

            if (theme.IsExternal || theme.IsDual)
            {
                if (!string.IsNullOrEmpty(editThemeDto.ExternalInstructorCode) && !IsNeptunCode(editThemeDto.ExternalInstructorCode))
                {
                    return new ServiceResponse<int> { Success = false, Message = "A megadott neptun kód formátuma nem megfelelő!" };
                }
                theme.ExternalInstructorCode = editThemeDto.ExternalInstructorCode;
                theme.ExternalInstructorName = editThemeDto.ExternalInstructorName;
                theme.ExternalCompanyId = editThemeDto.ExternalCompanyId;
            }
            else
            {
                theme.ExternalInstructorCode = string.Empty;
                theme.ExternalInstructorName = string.Empty;
                theme.ExternalCompanyId = null;
            }
            bool changedTheme = false;
            if (!oldTheme!.Equals(theme))
            {
                changedTheme = true;
            }
            List<int> selectedIds = editThemeDto.SelectedProgrammes;

            try
            {
                _context.Themes!.Update(theme);
                await _context.SaveChangesAsync();

                var currentIds = await _context.ThemeProgrammes!.Where(tp => tp.ThemeId == theme.Id).Select(tp => tp.ProgrammeId).ToListAsync();
                var toDelete = currentIds.Except(selectedIds);
                var toAdd = selectedIds.Except(currentIds);

                var themeprogrammes = await _context.ThemeProgrammes!.Where(tp => tp.ThemeId == theme.Id).ToListAsync();
                var programmes = await _context.Programmes!.ToListAsync();
                var approvals = await _context.Approvals!.ToListAsync();
                int semesterId = themeprogrammes.Any() ? themeprogrammes.First().SemesterId : throw new Exception("A semesterId nem lehet null");

                foreach (var delId in toDelete)
                {
                    var themeprogramme = themeprogrammes.FirstOrDefault(tp => tp.ProgrammeId == delId);
                    if (themeprogramme != null)
                    {
                        _context.ThemeProgrammes!.Remove(themeprogramme);
                        Console.WriteLine(delId + " deleted...");
                    }
                }
                foreach (var addId in toAdd)
                {
                    Programme? programme = programmes.FirstOrDefault(p => p.Id == addId) ?? throw new NullReferenceException("Programme is null"); ;
                    _context.ThemeProgrammes!.Add(new ThemeProgramme
                    {
                        Theme = theme,
                        ThemeId = theme.Id,
                        Programme = programme,
                        ProgrammeId = programme.Id,
                        Approval = approvals[0],
                        ApprovalId = approvals[0].Id,
                        SemesterId = semesterId,
                        DenyReason = string.Empty
                    });
                }
                if (changedTheme)
                {
                    foreach (var item in theme.ThemeProgrammes)
                    {
                        item.ApprovalId = approvals[0].Id;
                        item.DenyReason = string.Empty;
                        item.ApprovalDate = null;
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return new ServiceResponse<int> { Data = theme.Id, Success = false, Message = "Hiba történt a mentés közben!" };
            }

            var response = new ServiceResponse<int> { Data = theme.Id, Success = true, Message = "A módosítás mentve!" };

            return response;
        }

        public async Task<ServiceResponse<int>> CreateTheme(ThemeCreateDto themeDto)
        {
            if (themeDto.SelectedProgrammes.Count == 0)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Legalább egy szakon meg kell hirdetni a témát"
                };
            }

            string? checkProgrammes = await CheckProgrammes(themeDto.SelectedProgrammes);
            if (checkProgrammes != null)
            {
                return new ServiceResponse<int> { Success = false, Message = checkProgrammes };
            }

            var instructor = _context.Users!.FirstOrDefault(u => u.Username.Equals(themeDto.Instructor));
            if (instructor == null)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Témavezetőt meg kell adni!"
                };
            }

            var status = _context.Statuses!.FirstOrDefault(s => s.Id.Equals(1));
            if (status == null)
            {
                return new ServiceResponse<int> { Success = false, Message = "Hiba történt a téma meghirdetése során!" };
            }

            var theme = new Theme
            {
                Title = themeDto.Title,
                Description = themeDto.Description,
                InstructorId = instructor.Id,
                StatusId = status.Id,
                IsExternal = themeDto.IsExternal,
                IsDual = themeDto.IsDual,
                CreatedAt = DateTime.Now
            };

            if (theme.IsExternal || theme.IsDual)
            {
                if (!string.IsNullOrEmpty(themeDto.ExternalInstructorCode) && !IsNeptunCode(themeDto.ExternalInstructorCode))
                {
                    return new ServiceResponse<int> { Success = false, Message = "A megadott neptun kód formátuma nem megfelelő!" };
                }
                theme.ExternalInstructorCode = themeDto.ExternalInstructorCode;
                theme.ExternalInstructorName = themeDto.ExternalInstructorName;
                theme.ExternalCompanyId = themeDto.ExternalCompanyId;
            }
            else
            {
                theme.ExternalInstructorCode = string.Empty;
                theme.ExternalInstructorName = string.Empty;
                theme.ExternalCompanyId = null;
            }

            List<int> selectedIds = themeDto.SelectedProgrammes;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Themes!.Add(theme);
                    await _context.SaveChangesAsync();

                    var programmes = await _context.Programmes!.Where(p => !p.IsDeleted).ToListAsync();
                    var approval = await _context.Approvals!.FirstOrDefaultAsync(a => a.Name == "Jóváhagyásra vár");
                    var semester = await _context.Semesters!.FirstAsync(s => s.IsActive == true);

                    foreach (var addId in selectedIds)
                    {
                        Programme? programme = programmes.FirstOrDefault(p => p.Id == addId) ?? throw new NullReferenceException("Programme is null");
                        _context.ThemeProgrammes!.Add(new ThemeProgramme
                        {
                            Theme = theme,
                            ThemeId = theme.Id,
                            ProgrammeId = programme.Id,
                            ApprovalId = approval!.Id,
                            SemesterId = semester.Id,
                            DenyReason = string.Empty
                        });
                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    transaction.Rollback();
                    return new ServiceResponse<int> { Data = theme.Id, Success = false, Message = "Hiba történt a téma meghirdetése során!" };
                }
            }

            var response = new ServiceResponse<int> { Data = theme.Id, Success = true, Message = "Téma létrehozva!" };
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteTheme(int themeId, int userId, string userRole)
        {
            var theme = await _context.Themes!.FirstOrDefaultAsync(t => t.Id == themeId);
            if (theme == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "A téma nem található az adatbázisban!" };
            }

            if (userRole == Roles.Instructor && theme.InstructorId != userId)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Az oktató csak a saját témáját tudja törölni!" };
            }
            else if (userRole == Roles.HeadOfDepartment)
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userId);
                var instructor = await _context.Users!.FirstOrDefaultAsync(u => u.Id == theme.InstructorId);
                if (user!.DepartmentId != instructor!.DepartmentId)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "Tanszéki adminisztrátor csak a saját tanszékéhez tartozó témát tudja törölni!" };
                }
            }

            if (!string.IsNullOrEmpty(theme.StudentCode))
            {
                return new ServiceResponse<bool> { Success = false, Message = "A téma nem törölhető, mert van már hallgató hozzárendelve!" };
            }

            _context.Themes!.Remove(theme);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true};
        }

        public async Task<ServiceResponse<int>> ActivateTheme(int id)
        {
            if (!await ThemeExists(id))
            {
                Console.WriteLine("Theme not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen téma"
                };
            }

            Theme theme = await _context.Themes!.FindAsync(id);
            theme!.StatusId = 1;
            _context.Themes.Update(theme);
            _context.SaveChanges();
            return new ServiceResponse<int> { Data = 1 };
        }

        public async Task<ServiceResponse<int>> InactivateTheme(int id)
        {
            if (!await ThemeExists(id))
            {
                Console.WriteLine("Theme not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen téma"
                };
            }

            Theme theme = await _context.Themes!.FindAsync(id);
            theme!.StatusId = 2;
            _context.Themes.Update(theme);
            _context.SaveChanges();
            return new ServiceResponse<int> { Data = 2 };
        }

        public async Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammes()
        {
            var themeprogrammes = await _context.ThemeProgrammes!
                .Include(tp => tp.Theme!).ThenInclude(t => t.Instructor)
                .Include(tp => tp.Theme!).ThenInclude(t => t.Status)
                .Include(tp => tp.Approval)
                .Include(tp => tp.Programme)
                .Include(tp => tp.Semester)
                .ToListAsync();

            var response = new ServiceResponse<List<ThemeProgramme>>
            {
                Data = themeprogrammes
            };

            return response;
        }

        public async Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammesByProgramme(Programme programme, bool onlyUnderApproval = false)
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsActive);
            if (semester == null)
            {
                return new ServiceResponse<List<ThemeProgramme>>
                {
                    Success = false,
                    Message = "Jelenleg nincs témahirdetési időszak",
                    Data = null
                };
            }
            var querry = _context.ThemeProgrammes!
                .Include(tp => tp.Theme).ThenInclude(t => t!.Instructor)
                .Include(tp => tp.Theme).ThenInclude(t => t!.Status)
                .Include(tp => tp.Approval)
                .Include(tp => tp.Programme)
                .Include(tp => tp.Semester)
                .Where(tp => tp.SemesterId == semester.Id && tp.ProgrammeId == programme.Id && (tp.Theme!.StudentCode == null || tp.Theme!.StudentCode == ""));

            if (onlyUnderApproval)
            {
                querry = querry.Where(tp => tp.ApprovalId == 1);
            }
            var themeProgrammes = await querry.ToListAsync();

            var response = new ServiceResponse<List<ThemeProgramme>>
            {
                Message = themeProgrammes.Count == 0 ? "Nem találhatók jóváhagyandó témák" : null,
                Data = themeProgrammes
            };
            return response;
        }

        public async Task<ServiceResponse<List<ThemeProgramme>>> GetThemeProgrammesByDepartment(Department department)
        {
            List<ThemeProgramme>? themeprogrammes;
            ServiceResponse<List<ThemeProgramme>> response;
            try
            {
                themeprogrammes = await _context.ThemeProgrammes!.Where(tp => tp.Theme!.Instructor!.DepartmentId == department.Id)
                .Include(tp => tp.Theme!).ThenInclude(t => t.Instructor)
                .Include(tp => tp.Theme!).ThenInclude(t => t.Status)
                .Include(tp => tp.Approval)
                .Include(tp => tp.Programme)
                .Include(tp => tp.Semester)
                .ToListAsync();

                response = new ServiceResponse<List<ThemeProgramme>>
                {
                    Data = themeprogrammes
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<List<ThemeProgramme>>
                {
                    Data = null,
                    Success = false,
                    Message = "Hiba történt a témák lekérdezése közben!"
                };
            }
            return response;
        }

        public async Task<ServiceResponse<int>> ApproveThemeProgramme(ThemeProgrammeFeedbackDto tpDto)
        {
            var approval = await _context.Approvals!.FindAsync(2);
            if (approval == null)
            {
                return new ServiceResponse<int> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }
            var themeprogramme = await _context.ThemeProgrammes!
                .Include(tp => tp.Programme)
                .Include(tp => tp.Theme)
                .FirstOrDefaultAsync(tp => tp.ThemeId == tpDto.ThemeId && tp.ProgrammeId == tpDto.ProgrammeId && tp.ApprovalId == 1);
            if (themeprogramme is null)
            {
                return new ServiceResponse<int> { Success = false, Message = "Nem található a téma" };
            }
            else if (!string.IsNullOrEmpty(themeprogramme?.Theme?.Student) || !string.IsNullOrEmpty(themeprogramme?.Theme?.StudentCode))
            {// [JAVITANI]
                return new ServiceResponse<int> { Success = false, Message = "Jóváhagyás sikertelen: a témához már van hallgató rendelve!" };
            }
            themeprogramme!.ApprovalId = approval!.Id;
            themeprogramme.Approval = approval;
            themeprogramme.ApprovalDate = DateTime.Now;
            _context.ThemeProgrammes.Update(themeprogramme);
            _context.SaveChanges();
            return new ServiceResponse<int> { Success = true, Message = "Téma jóváhagyva a(z) " + themeprogramme.Programme?.Name + " szakon!" };
        }

        public async Task<ServiceResponse<int>> DenyThemeProgramme(ThemeProgrammeFeedbackDto tpDto)
        {
            var approval = await _context.Approvals!.FindAsync(3);
            if (approval == null)
            {
                return new ServiceResponse<int> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }
            var themeprogramme = await _context.ThemeProgrammes!
                .Include(tp => tp.Programme)
                .Include(tp => tp.Theme)
                .FirstOrDefaultAsync(tp => tp.ThemeId == tpDto.ThemeId && tp.ProgrammeId == tpDto.ProgrammeId && tp.ApprovalId == 1);
            if (themeprogramme is null)
            {
                return new ServiceResponse<int> { Success = false, Message = "Nem található a téma" };
            }
            if (!string.IsNullOrEmpty(themeprogramme?.Theme?.Student) || !string.IsNullOrEmpty(themeprogramme?.Theme?.StudentCode))
            {// [JAVITANI]
                return new ServiceResponse<int> { Success = false, Message = "Elutasítás sikertelen: a témához már van hallgató rendelve!" };
            }
            themeprogramme!.ApprovalId = approval!.Id;
            themeprogramme.Approval = approval;
            themeprogramme.DenyReason = tpDto.DenyReason;
            themeprogramme.ApprovalDate = DateTime.Now;
            _context.ThemeProgrammes!.Update(themeprogramme);
            _context.SaveChanges();
            return new ServiceResponse<int> { Success = true, Message = "Téma elutasítva a(z) " + themeprogramme.Programme?.Name + " szakon!" };
        }

        public async Task<ServiceResponse<List<Theme>>> GetThemeProgrammesForStudents()
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsActive && s.IsVisibleStudent);
            if (semester == null)
            {
                return new ServiceResponse<List<Theme>>
                {
                    Success = false,
                    Message = "Jelenleg nincs témahirdetési időszak",
                    Data = null
                };
            }
            var themes = await _context.Themes!
                .Include(t => t.Instructor!)
                .ThenInclude(i => i.Programme!)
                .ThenInclude(p => p.ThemeProgrammes!)
                .ThenInclude(p => p.Approval)
                .Include(t => t.Status)
                .Include(t => t.StudentProgramme)
                .Where(t => t.StatusId == 1 && t.ThemeProgrammes!.Any(tp => tp.ApprovalId == 2 && tp.SemesterId == semester.Id))
                .Select(t => new Theme
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Instructor = t.Instructor,
                    ThemeProgrammes = t.ThemeProgrammes!.Where(tp => tp.ApprovalId == 2 && tp.SemesterId == semester.Id)
                    .Select(tp => new ThemeProgramme
                    {
                        Programme = tp.Programme,
                    }).ToList(),
                    StudentCode = t.StudentCode,
                    IsDual = t.IsDual,
                    IsExternal = t.IsExternal
                })
                .ToListAsync();

            var response = new ServiceResponse<List<Theme>>
            {
                Message = themes.Count == 0 ? "Jelenleg nincsenek témák meghirdetve" : null,
                Data = themes
            };
            return response;
        }

        public async Task<ServiceResponse<Theme>> GetThemeByIdForStudent(int id)
        {
            var semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsActive && s.IsVisibleStudent);
            if (semester == null)
            {
                return new ServiceResponse<Theme>
                {
                    Success = false,
                    Message = "Jelenleg nincs témahirdetési időszak",
                    Data = null
                };
            }

            var data = await _context.Themes!
                .Include(t => t.ThemeProgrammes!).ThenInclude(tp => tp.Programme)
                .Include(t => t.Students)
                .Include(t => t.Instructor)
                .Where(t => t.Id == id && t.StatusId == 1 && t.ThemeProgrammes!.Any(tp => tp.ApprovalId == 2 && tp.SemesterId == semester.Id))
                .FirstOrDefaultAsync();

            var response = new ServiceResponse<Theme>
            {
                Success = data is not null,
                Data = data,
                Message = data is null ? "Nem található a téma" : null
            };
            return response;
        }

        public async Task<ServiceResponse<ThemeForAssignStudentDto>> GetThemeForAssignStudent(int themeId)
        {
            var approval = await _context.Approvals!.FirstOrDefaultAsync(a => a.Name == "Jóváhagyva");
            if (approval == null)
            {
                return new ServiceResponse<ThemeForAssignStudentDto> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }

            var theme = await _context.Themes!
                .Include(t => t.ThemeProgrammes!.Where(p => p.ApprovalId == approval.Id))!.ThenInclude(p => p.Programme)
                .FirstOrDefaultAsync(t => t.Id == themeId);

            if (theme == null)
            {
                return new ServiceResponse<ThemeForAssignStudentDto> { Success = false, Message = "A téma nem található az adatbázisban!" };
            }

            var students = await _context.Students!.Where(s => s.ThemeId == themeId).ToArrayAsync();

            var response = new ServiceResponse<ThemeForAssignStudentDto>
            {
                Success = true,
                Data = new ThemeForAssignStudentDto
                {
                    ThemeId = theme.Id,
                    Title = theme.Title,
                    Students = students,
                    ThemeProgrammes = theme.ThemeProgrammes!.Select(tp => new ProgrammeDto { Id = tp.ProgrammeId, Name = tp.Programme!.Name }).ToList()
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> AssignStudent(AssignStudentDto assignStudentDto, int userId, string userRole)
        {
            var activeStatus = await _context.Statuses!.FirstOrDefaultAsync(s => s.Name == "Aktív");
            if (activeStatus == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }

            var theme = await _context.Themes!.Include(t => t.ThemeProgrammes)
                .FirstOrDefaultAsync(t => t.Id == assignStudentDto.ThemeId && t.StatusId == activeStatus.Id && t.ThemeProgrammes
                    .Any(tp => tp.ApprovalId == 2 && tp.ProgrammeId == assignStudentDto.StudentProgrammeId));

            if (theme == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "A téma nem található az adatbázisban!" };
            }

            if (userRole == Roles.Instructor && theme.InstructorId != userId)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Az oktató csak a saját témájához tud hallgatót rendelni!" };
            }
            else if (userRole == Roles.HeadOfDepartment)
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userId);
                var instructor = await _context.Users!.FirstOrDefaultAsync(u => u.Id == theme.InstructorId);
                if (user!.DepartmentId != instructor!.DepartmentId)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "Tanszéki adminisztrátor csak a saját tanszékéhez tartozó témához tud hallgatót rendelni!" };
                }
            }

            var maxStudent = _configuration.GetValue<int>("AppSettings:MaxStudentOneTheme");
            if (!string.IsNullOrWhiteSpace(theme.StudentCode))
            {
                theme = await _context.Themes!.Include(t => t.Students).Include(t => t.ThemeProgrammes).FirstOrDefaultAsync(t => t.Id == assignStudentDto.ThemeId && t.StatusId == activeStatus.Id);
                if (theme.Students.ToList().Count == maxStudent)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "A témához már hozzá van rendelve a maximális számú hallgató!" };
                }
                else if (theme.Students.Any(s => s.NeptunCode == assignStudentDto.StudentCode) || theme.StudentCode == assignStudentDto.StudentCode)
                {
                    Console.WriteLine($"The neptun code({assignStudentDto.StudentCode}) is already added");
                    return new ServiceResponse<bool> { Success = false, Message = "A hallgató már hozzá van rendelve a témához!" };
                }
            }
            var semesterId = theme.ThemeProgrammes!.First().SemesterId;

            var studentIsAssignOtherTheme = await _context.Students!
                .Include(s => s.Theme).ThenInclude(t => t.ThemeProgrammes)
                .AnyAsync(s =>
                    s.Id != assignStudentDto.ThemeId &&
                    s.NeptunCode.ToUpper() == assignStudentDto.StudentCode.ToUpper() &&
                    s.Theme!.ThemeProgrammes.Any(tp => tp.SemesterId == semesterId));

            if (studentIsAssignOtherTheme)
            {
                return new ServiceResponse<bool> { Success = false, Message = "A hallgató már hozzá van rendelve másik témához a félévben!" };
            }

            if (!IsNeptunCode(assignStudentDto.StudentCode))
            {
                return new ServiceResponse<bool> { Success = false, Message = "A megadott neptun kód formátuma nem megfelelő!" };
            }

            if (string.IsNullOrWhiteSpace(theme.StudentCode))
            {
                theme.Student = assignStudentDto.Student;
                theme.StudentCode = assignStudentDto.StudentCode.ToUpper();
                theme.StudentProgrammeId = assignStudentDto.StudentProgrammeId;
                theme.Students = new List<Student>()
                {
                    new Student
                    {
                        NeptunCode = assignStudentDto.StudentCode.ToUpper(),
                        ProgrammeId = assignStudentDto.StudentProgrammeId,
                        SemesterId = semesterId,
                        StudentName = assignStudentDto.Student,
                        ThemeId = assignStudentDto.ThemeId
                    }
                };
            }
            else
            {
                theme.Student = default;
                theme.StudentCode = "több";
                theme.StudentProgrammeId = default;
                theme.Students.Add(new Student
                {
                    NeptunCode = assignStudentDto.StudentCode.ToUpper(),
                    ProgrammeId = assignStudentDto.StudentProgrammeId,
                    SemesterId = semesterId,
                    StudentName = assignStudentDto.Student,
                    ThemeId = assignStudentDto.ThemeId
                });
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Success = true, Message = "Sikeres hallgató hozzárendelés!" };
        }

        public async Task<ServiceResponse<bool>> RemoveStudent(RemoveStudentDto removeStudentDto, int userId, string userRole)
        {
            var activeStatus = await _context.Statuses!.FirstOrDefaultAsync(s => s.Name == "Aktív");
            if (activeStatus == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }

            var activeSemester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsActive);
            if (activeSemester == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Hiba a beállításokban, kérem vegye fel a kapcsolatot a rendszergazdával!" };
            }

            var theme = await _context.Themes!.FirstOrDefaultAsync(t => t.Id == removeStudentDto.ThemeId && t.StatusId == activeStatus.Id);
            if (theme == null)
            {
                return new ServiceResponse<bool> { Success = false, Message = "A téma nem található az adatbázisban!" };
            }
            if (userRole == Roles.Instructor && theme.InstructorId != userId)
            {
                return new ServiceResponse<bool> { Success = false, Message = "Az oktató csak a saját témájához tud hallgatót rendelni!" };
            }
            else if (userRole == Roles.HeadOfDepartment)
            {
                var user = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userId);
                var instructor = await _context.Users!.FirstOrDefaultAsync(u => u.Id == theme.InstructorId);
                if (user!.DepartmentId != instructor!.DepartmentId)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "Tanszéki adminisztrátor csak a saját tanszékéhez tartozó témához tud hallgatót rendelni!" };
                }
            }

            if (theme.StudentCode.Equals("több"))
            {
                theme = await _context.Themes!.Include(t => t.Students).FirstOrDefaultAsync(t => t.Id == removeStudentDto.ThemeId && t.StatusId == activeStatus.Id);
                if (!theme.Students.Any(s => s.NeptunCode == removeStudentDto.StudentCode))
                {
                    return new ServiceResponse<bool> { Success = false, Message = "A megadott hallgató nincs ehhez a témához rendelve!" };
                }
            }
            else
            {
                if (theme.StudentCode != removeStudentDto.StudentCode)
                {
                    return new ServiceResponse<bool> { Success = false, Message = "A megadott hallgató nincs ehhez a témához rendelve!" };
                }
            }
            var deletedUser = await _context.Students!.FirstAsync(s => s.NeptunCode == removeStudentDto.StudentCode && s.ThemeId == removeStudentDto.ThemeId);
            if (!theme.StudentCode.Equals("több"))
            {
                theme.Student = default;
                theme.StudentCode = default;
                theme.StudentProgrammeId = default;
            }
            else
            {
                if (theme.Students!.Count == 2)
                {
                    var remaininStudent = await _context.Students!.FirstAsync(s => s.NeptunCode != removeStudentDto.StudentCode && s.ThemeId == removeStudentDto.ThemeId);
                    theme.Student = remaininStudent.StudentName;
                    theme.StudentCode = remaininStudent.NeptunCode;
                    theme.StudentProgrammeId = remaininStudent.ProgrammeId;
                }
                else
                {
                    theme.Student = default;
                    theme.StudentCode = "több";
                    theme.StudentProgrammeId = default;
                }
            }

            _context.Students!.Remove(deletedUser);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Success = true, Message = "A hallgató sikeresen eltávolítva a témáról!" };
        }

        public async Task<ServiceResponse<bool>> CopyThemeForAdmin(CopyThemeDto copyThemeDto)
        {
            var themes = new List<Theme>();
            foreach (var item in copyThemeDto.Themes)
            {
                var theme = await _context.Themes!.Include(t => t.ThemeProgrammes).FirstOrDefaultAsync(t => t.Id == item);
                if (theme != null)
                {
                    themes.Add(theme);
                }
            }
            return await CopyTheme(themes, copyThemeDto);
        }

        public async Task<ServiceResponse<bool>> CopyThemeForInstructor(CopyThemeDto copyThemeDto, int instructorId)
        {
            var themes = new List<Theme>();
            foreach (var item in copyThemeDto.Themes)
            {
                var theme = await _context.Themes!.Include(t => t.ThemeProgrammes).FirstOrDefaultAsync(t => t.Id == item && t.InstructorId == instructorId);
                if (theme != null)
                {
                    themes.Add(theme);
                }
            }
            return await CopyTheme(themes, copyThemeDto);
        }

        public async Task<ServiceResponse<bool>> CopyThemeForHeadOfDepartment(CopyThemeDto copyThemeDto, int departmentId)
        {
            var themes = new List<Theme>();
            foreach (var item in copyThemeDto.Themes)
            {
                var theme = await _context.Themes!.Include(t => t.ThemeProgrammes).Include(t => t.Instructor).FirstOrDefaultAsync(t => t.Id == item && t.Instructor.DepartmentId == departmentId);
                if (theme != null)
                {
                    themes.Add(theme);
                }
            }
            return await CopyTheme(themes, copyThemeDto);
        }

        public async Task<ServiceResponse<bool>> CopyTheme(List<Theme> themes, CopyThemeDto copyThemeDto)
        {
            if (!themes.Any())
            {
                return new ServiceResponse<bool> { Success = false, Data = false, Message = "Nem található téma" };
            }

            var fromSemester = await _context.Semesters!.FirstOrDefaultAsync(s => s.Id == copyThemeDto.FromSemester);
            var toSemester = await _context.Semesters!.FirstOrDefaultAsync(s => s.Id == copyThemeDto.ToSemester);
            if (fromSemester == null || toSemester == null)
            {
                return new ServiceResponse<bool> { Success = false, Data = false, Message = "Nem található a félév" };
            }

            var themelist = await _context.ThemeProgrammes!
                .Include(tp => tp.Theme)
                .Where(tp => tp.SemesterId == toSemester.Id).ToListAsync();

            foreach (var oldTheme in themes)
            {
                var theme = new Theme(oldTheme, toSemester: toSemester.Id);
                if (themelist.Any(tp => tp.Theme.SourceThemeId == oldTheme.Id))
                {
                    return new ServiceResponse<bool> { Success = false, Data = false, Message = $"A '{oldTheme.Title}' nevű téma már létezik a '{toSemester.Name}' nevű félévben" };
                }
                await _context.Themes!.AddAsync(theme);
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Success = true, Data = true, Message = "" };
        }

        public async Task<string?> CheckProgrammes(List<int> programmeIds)
        {
            var list = await _context.Programmes!.Where(p => programmeIds.Contains(p.Id)).ToListAsync();
            if (list.Count != programmeIds.Count)
            {
                return "Nem található meg a szak!";
            }
            else if (!list.All(p => p.ProgrammeType.Equals(list[0].ProgrammeType)))
            {
                return "A téma nem hirdethető egyszerre BSc és MSc szakon is!";
            }
            else
            {
                return null;
            }
        }
        public static bool IsNeptunCode(string neptunCode)
        {
            var regex = new Regex("^[A-Z0-9]{6}$");
            return regex.Match(neptunCode.ToUpper()).Success;
        }
        public async Task<bool> ThemeExists(int id)
        {
            if (await _context.Themes!.AnyAsync(t => t.Id == id)) return true;
            return false;
        }
    }
}
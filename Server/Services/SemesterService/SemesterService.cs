using PannonBlazor.Shared.Models;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Services.SemesterService
{
    public class SemesterService : ISemesterService
    {
        private readonly DataContext _context;

        public SemesterService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<int>> ActivateSemester(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            var semesters = await _context.Semesters!.Where(s => s.IsActive == true).ToListAsync();
            foreach(var sem in semesters)
            {
                sem.IsVisibleInstructor = false;
                sem.IsVisibleStudent = false;
                sem.IsActive = false;
            }
            _context.UpdateRange(semesters);
            Semester? semester = await _context.Semesters!.FindAsync(id);
            semester!.IsActive = true;
            _context.Semesters.Update(semester);
            _context.SaveChanges();
            return new ServiceResponse<int> {Data = semester.Id };
        }

        public async Task<ServiceResponse<int>> AllowInstructor(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            Semester? semester = await _context.Semesters!.FindAsync(id);
            semester!.IsVisibleInstructor = true;
            _context.Semesters.Update(semester);
            _context.SaveChanges();
            return new ServiceResponse<int> {Data = semester.Id };
        }

        public async Task<ServiceResponse<int>> AllowStudent(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            Semester? semester = await _context.Semesters!.FindAsync(id);
            semester!.IsVisibleStudent = true;
            _context.Semesters.Update(semester);
            _context.SaveChanges();
            return new ServiceResponse<int> {Data = semester.Id };
        }

        public async Task<ServiceResponse<int>> CreateSemester(SemesterDto semester)
        {
            if (await SemesterExists(semester.Id))
            {
                Console.WriteLine("Semester already exists");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Már van ilyen időszak"
                };
            }

            if (await SemesterExists(semester.StartDate, semester.EndDate) == null)
            {
                var newSemester = new Semester
                {
                    Name = semester.Name,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate
                };

                _context.Semesters!.Add(newSemester);
                await _context.SaveChangesAsync();

                var response = new ServiceResponse<int> { Data = newSemester.Id, Success = true, Message = "Időszak létrehozva!" };

                return response;
            }
            Console.WriteLine("Semester exists this range");
            return new ServiceResponse<int>
            {
                Success = false,
                Message = "Már van rögzíve időszak ebben az időtartományban"
            };
        }

        public async Task<ServiceResponse<int>> DeleteSemester(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }

            var semester = await _context.Semesters!.FindAsync(id);

            _context.Semesters.Remove(semester!);
            _context.SaveChanges();

            return new ServiceResponse<int> { Data = id};
        }

        public async Task<ServiceResponse<int>> DenyInstructor(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            Semester? semester = await _context.Semesters!.FindAsync(id);
            semester!.IsVisibleInstructor = false;
            _context.Semesters.Update(semester);
            _context.SaveChanges();
            return new ServiceResponse<int> { Data = semester.Id};
        }

        public async Task<ServiceResponse<int>> DenyStudent(int id)
        {
            if (!await SemesterExists(id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            Semester? semester = await _context.Semesters!.FindAsync(id);
            semester!.IsVisibleStudent = false;
            _context.Semesters.Update(semester);
            _context.SaveChanges();
            return new ServiceResponse<int> { Data = semester.Id};
        }

        public async Task<ServiceResponse<int>> EditSemester(SemesterDto semester)
        {
            if (!await SemesterExists(semester.Id))
            {
                Console.WriteLine("Semester not found");
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Nincs ilyen időszak!"
                };
            }
            var semesterCheck = await SemesterExists(semester.StartDate, semester.EndDate);
            var updateSemester = await _context.Semesters!.FindAsync(semester.Id);

            if (semesterCheck == null || semesterCheck.Id.Equals(updateSemester.Id))
            {
                updateSemester!.Name = semester.Name;
                updateSemester!.StartDate = semester.StartDate;
                updateSemester!.EndDate = semester.EndDate;

                _context.Semesters.Update(updateSemester);
                _context.SaveChanges();

                return new ServiceResponse<int> { Data = updateSemester.Id, Success = true, Message = "Időszak módosítva" };
            }
            Console.WriteLine("Semester exists this range");
            return new ServiceResponse<int>
            {
                Success = false,
                Message = "Már van rögzíve időszak ebben az időtartományban"
            };
        }

        public async Task<ServiceResponse<Semester>> GetActiveSemester()
        {
            Semester? semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.IsActive == true);
            if (semester == null)
            {
                return new ServiceResponse<Semester> 
                {
                    Data = null, 
                    Success = false,
                    Message = "Jelenleg nincs témahirdetési időszak"
                };
            }
            return new ServiceResponse<Semester>
            {
                Data = semester
            };
        }

        public async Task<ServiceResponse<Semester>> GetSemesterById(int id)
        {
            Semester? semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.Id.Equals(id));
            if (semester == null)
            {
                return new ServiceResponse<Semester>
                {
                    Data = null,
                    Success = false,
                    Message = "Nincs ilyen időszak"
                };
            }
            return new ServiceResponse<Semester>
            {
                Data = semester
            };
        }

        public async Task<ServiceResponse<Semester>> GetSemesterForInstructorsById(int id)
        {
            Semester? semester = await _context.Semesters!.FirstOrDefaultAsync(s => s.Id.Equals(id) && s.IsVisibleInstructor);
            if (semester == null)
            {
                return new ServiceResponse<Semester>
                {
                    Data = null,
                    Success = false,
                    Message = "Nincs ilyen időszak"
                };
            }
            return new ServiceResponse<Semester>
            {
                Data = semester
            };
        }

        public async Task<ServiceResponse<List<Semester>>> GetSemestersAsync()
        {
            var response = new ServiceResponse<List<Semester>>
            {
                Data = await _context.Semesters!.Include(s => s.ThemeProgrammes).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Semester>>> GetSemestersForInstructorsAsync()
        {
            var response = new ServiceResponse<List<Semester>>
            {
                Data = await _context.Semesters!.Where(s=>s.IsVisibleInstructor)
                .Select(s=> new Semester
                {
                    Id= s.Id,
                    EndDate= s.EndDate,
                    StartDate= s.StartDate,
                    IsActive = s.IsActive,
                    Name= s.Name
                    
                }).ToListAsync()
            };

            return response;
        }

        public async Task<bool> SemesterExists(int id)
        {
            if (await _context.Semesters!.AnyAsync(s => s.Id == id)) return true;
            return false;
        }

        public async Task<Semester?> SemesterExists(DateTime start, DateTime end)
        {
            return await _context.Semesters!.FirstOrDefaultAsync(s => s.StartDate <= end && s.EndDate >= start);
        }
    }
}

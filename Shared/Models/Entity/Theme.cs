using System.ComponentModel.DataAnnotations.Schema;
using PannonBlazor.Shared.Models.Dto;

namespace PannonBlazor.Shared.Models.Entity
{
    public class Theme
    {
        public Theme() { }
        public Theme(Theme theme, int id = default, int toSemester = default)
        {
            Id = id;
            Title = theme.Title;
            Description = theme.Description;
            Instructor = theme.Instructor;
            InstructorId = theme.InstructorId;
            Status = theme.Status;
            StatusId = theme.StatusId;
            Student = id == default ? null : theme.Student;
            StudentCode = id == default ? null : theme.StudentCode;
            StudentProgrammeId = id == default ? null : theme.StudentProgrammeId;
            StudentProgramme = id == default ? null : theme.StudentProgramme;
            IsExternal = theme.IsExternal;
            IsDual = theme.IsDual;
            ExternalInstructorCode = theme.ExternalInstructorCode;
            ExternalInstructorName = theme.ExternalInstructorName;
            ExternalCompanyId = theme.ExternalCompanyId;
            ExternalCompany = theme.ExternalCompany;
            if (toSemester == default)
            {
                ThemeProgrammes = theme.ThemeProgrammes;
            }
            else
            {
                ThemeProgrammes = new List<ThemeProgramme>();
                foreach (var themeProgramme in theme.ThemeProgrammes!)
                {
                    ThemeProgrammes.Add(new ThemeProgramme(themeProgramme, toSemester));
                }
            }
            CreatedAt = id == default ? DateTime.Now : theme.CreatedAt;
            SourceThemeId = id == default ? theme.Id : null;
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public User? Instructor { get; set; }
        public int InstructorId { get; set; }
        public Status? Status { get; set; }
        public int StatusId { get; set; }
        public string? Student { get; set; } = string.Empty;
        public string? StudentCode { get; set; } = string.Empty;
        public int? StudentProgrammeId { get; set; } = null;
        public Programme? StudentProgramme { get; set; }
        public List<Student>? Students { get; set; }

        public bool IsExternal { get; set; }
        public bool IsDual { get; set; }
        public string ExternalInstructorCode { get; set; } = string.Empty;
        public string ExternalInstructorName { get; set; } = string.Empty;

        [ForeignKey("ExternalCompanyId")]
        public int? ExternalCompanyId { get; set; }
        public Company? ExternalCompany { get; set; }

        public List<ThemeProgramme>? ThemeProgrammes { get; set; }

        public DateTime? CreatedAt { get; set; }

        [ForeignKey("SourceThemeId")]
        public int? SourceThemeId { get; set; }
        public Theme? SourceTheme { get; set; }

        public bool Equals(Theme obj)
        {
            return Id == obj.Id &&
                Title == obj.Title &&
                Description == obj.Description &&
                InstructorId == obj.InstructorId &&
                StatusId == obj.StatusId &&
                IsExternal == obj.IsExternal &&
                IsDual == obj.IsDual &&
                ExternalInstructorCode == obj.ExternalInstructorCode &&
                ExternalInstructorName == obj.ExternalInstructorName &&
                ExternalCompanyId == obj.ExternalCompanyId;
        }
    }
}

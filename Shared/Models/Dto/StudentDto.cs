using System.ComponentModel.DataAnnotations;

namespace PannonBlazor.Shared.Models.Dto
{
    public class StudentDto
    {
        [Display(Name = "Hallgató neve")]
        public string StudentName { get; set; } = string.Empty;
        [Display(Name = "Hallgató neptun kódja")]
        public string NeptunCode { get; set; } = string.Empty;
        [Display(Name = "Szak neve")]
        public string ProgrammeName { get; set; } = string.Empty;
    }

    public class ThemeStudentShowDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public List<string>? NeptunCodes { get; set; }
        public string? NeptunCode { get; set; } = string.Empty;
        public string ThemeType { get; set; }
        public List<ProgrammeDto> Programmes { get; set; }
    }

    public class StudentMultipleNeptunDto : StudentDto
    {
        [Display(Name = "Téma azonosítója")]
        public int ThemeId { get; set; }
        [Display(Name = "Téma címe")]
        public string ThemeTitle { get; set; } = string.Empty;
        [Display(Name = "Téma leírása")]
        public string ThemeDescription { get; set; } = string.Empty;
        [Display(Name = "Téma Típus")]
        public string ThemeType { get; set; } = string.Empty;
        [Display(Name = "Téma Létrehozva")]
        public DateTime ThemeCreatedAt { get; set; }
        [Display(Name = "Oktató email címe")]
        public string InstructorEmail { get; set; } = string.Empty;
        [Display(Name = "Oktató neve")]
        public string InstructorName { get; set; } = string.Empty;
        [Display(Name = "Félév")]
        public string SemesterName { get; set; } = string.Empty;
    }

    public class StudentThemeListDto
    {
        [Display(Name = "Téma azonosítója")]
        public int Id { get; set; }
        [Display(Name = "Téma címe")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "Oktató neve")]
        public string InstructorName { get; set; } = string.Empty;
        [Display(Name = "Típus")]
        public string ThemeType { get; set; } = string.Empty;
        [Display(Name = "Hallgató neptun kódja")]
        public string NeptunCode { get; set; } = string.Empty;
        [Display(Name = "Szakok")]
        public List<ProgrammeDto> Programmes { get; set; } = new List<ProgrammeDto>();
    }
}

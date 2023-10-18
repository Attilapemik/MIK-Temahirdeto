using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PannonBlazor.Shared.Models.Dto
{
    public class ThemeProgrammeDto
    {
        [Display(Name = "Téma azonosítója")]
        public int ThemeId { get; set; }
        [Display(Name = "Téma címe")]
        public string ThemeTitle { get; set; } = string.Empty;
        [Display(Name = "Téma leírása")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Létrehozva")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Oktató email címe")]
        public string Instructor { get; set; } = string.Empty;
        [Display(Name = "Oktató neve")]
        public string InstructorName { get; set; } = string.Empty;
        [Display(Name = "Hallgató neve")]
        public string Student { get; set; } = string.Empty;
        [Display(Name = "Hallgató neptun kódja")]
        public string StudentCode { get; set; } = string.Empty;
        [Display(Name = "Félév")]
        public string SemesterName { get; set; } = string.Empty;
        [Display(Name = "Státusz")]
        public string StatusName { get; set; } = string.Empty;
        [Display(Name = "Szak azonosítója")]
        public int ProgrammeId { get; set; }
        [Display(Name = "Szak neve")]
        public string ProgrammeName { get; set; } = string.Empty;
        [Display(Name = "Jóváhagyva/elutasítva")]
        public string Approval { get; set; } = string.Empty;
        [Display(Name = "Jóváhagyás/elutasítás dátuma")]
        public DateTime? ApprovalDate { get; set; }
        [Display(Name = "Típus")]
        public string ThemeType { get; set; } = string.Empty;
        public string DenyReason { get; set; } = string.Empty;
    }
}

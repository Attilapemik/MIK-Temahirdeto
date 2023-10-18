using System.ComponentModel.DataAnnotations;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Shared.Models.Dto
{
    public class ThemeForAssignStudentDto
    {
        public int ThemeId { get; set; }
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(150, ErrorMessage = "Maximum 150 karakter adható meg")]
        public string Student { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pontosan 6 karakter adható meg")]
        [RegularExpression("^[A-Z0-9]{6}$", ErrorMessage = "A NEPTUN kód csak az angol ABC nagybetűit, illetve számokat tartalmazhat")]
        public string StudentCode { get; set; } = string.Empty;
        public int StudentProgrammeId { get; set; } = default;
        public IList<Student> Students { get; set; }
        public IList<ProgrammeDto> ThemeProgrammes { get; set; } = new List<ProgrammeDto>();
    }

    public class AssignStudentDto
    {
        public int ThemeId { get; set; }
        public string Student { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public int StudentProgrammeId { get; set; } = default;
    }

    public class RemoveStudentDto
    {
        public int ThemeId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
    }
}

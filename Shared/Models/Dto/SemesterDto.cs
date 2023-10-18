using System.ComponentModel.DataAnnotations;

namespace PannonBlazor.Shared.Models.Dto
{
    public class SemesterDto : SemesterGetDto
    {
        public bool IsVisibleInstructor { get; set; }
        public bool IsVisibleStudent { get; set; }
        public int ThemeProgrammesCount { get; set; }
    }

    public class SemesterGetDto
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(100, ErrorMessage = "Maximum 100 karakter adható meg")]
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = false;
    }
}

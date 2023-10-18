using System.ComponentModel.DataAnnotations;
using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Shared.Models.Dto
{
    public class ThemeDto : ThemeEditDto
    {
        public string Instructor { get; set; } = string.Empty;
        public string InstructorEmail { get; set; } = string.Empty;
        public string SemesterName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;

        public int NumberOfFeedback { get; set; }
        public int NumberOfApproved { get; set; }
        public int NumberOfProgrammes { get; set; }
        public bool IsRejected { get; set; } = false;

        public string StudentProgramme { get; set; } = string.Empty;
        public string Student { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public List<Student>? Students { get; set; } = new List<Student>();

        public string ExternalCompanyName { get; set; } = string.Empty;

        public List<ThemeProgrammeDto> ThemeProgrammes { get; set; } = new List<ThemeProgrammeDto>();
    }

    public class ThemeCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        public string Title { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(5000, ErrorMessage = "Maximum 5000 karakter adható meg")]
        public string Description { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = true, ErrorMessage = "A mező megadása kötelező")]
        public string Instructor { get; set; } = string.Empty;

        public List<int> SelectedProgrammes { get; set; } = new List<int>();
        public bool IsExternal { get; set; }
        public bool IsDual { get; set; }
        public string ExternalInstructorCode { get; set; } = string.Empty;
        public string ExternalInstructorName { get; set; } = string.Empty;
        public int? ExternalCompanyId { get; set; } = null;
    }

    public class ThemeEditDto
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        public string Title { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(5000, ErrorMessage = "Maximum 5000 karakter adható meg")]
        public string Description { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        public string InstructorName { get; set; } = string.Empty;
        public List<int> SelectedProgrammes { get; set; } = new List<int>();

        public bool IsExternal { get; set; }
        public bool IsDual { get; set; }
        public string ExternalInstructorCode { get; set; } = string.Empty;
        public string ExternalInstructorName { get; set; } = string.Empty;
        public int? ExternalCompanyId { get; set; } = null;

        public DateTime CreatedAt { get; set; }
        public string ThemeType
        {
            get
            {
                if (IsDual)
                {
                    return ThemeTypes.Dual;
                }
                if (IsExternal)
                {
                    return ThemeTypes.External;
                }
                return ThemeTypes.Internal;
            }
        }
    }
}

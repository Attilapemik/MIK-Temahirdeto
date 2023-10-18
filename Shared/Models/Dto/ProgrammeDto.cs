using PannonBlazor.Shared.Models.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PannonBlazor.Shared.Models.Dto
{
    public class ProgrammeDto : ProgrammeCreateDto
    {
        public int Id { get; set; }
    }
    public class ProgrammeCreateDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "A mező megadása kötelező")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Nem megfelelő a szín formátuma")]
        public string Color { get; set; } = "#906090";
        [Required(ErrorMessage = "A mező megadása kötelező")]
        public ProgrammeType ProgrammeType { get; set; }
    }
}

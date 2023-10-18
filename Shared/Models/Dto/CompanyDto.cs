using System.ComponentModel.DataAnnotations;

namespace PannonBlazor.Shared.Models.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        public string Name { get; set; } = string.Empty;
    }
}

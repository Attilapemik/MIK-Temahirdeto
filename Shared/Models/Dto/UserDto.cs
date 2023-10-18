using PannonBlazor.Shared.Constans;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PannonBlazor.Shared.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(100, ErrorMessage = "Maximum 100 karakter adható meg")]
        public string Username { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^[a-zA-Z0-9]{6}$", ErrorMessage = Messages.WrongNeptunCode), AllowNull]
        public string NeptunCode { get; set; } = string.Empty;
        public IList<string> RoleIds { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProgrammeId { get; set; }
        public string LdapUid { get; set; }
    }
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? NeptunCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; }
        public string? ProgrammeName { get; set; }
        public IList<string> Roles { get; set; }

        public string GetRolesString
        {
            get
            {
                if (ProgrammeName == null)
                {
                    return string.Join(", ", Roles);
                }
                else
                {
                    string roles = "";
                    foreach (var role in Roles)
                    {
                        if (role.Equals("Szakfelelős"))
                        {
                            roles += $"{(Roles[0] == role ? "" : ", ")}{role}({ProgrammeName})";
                        }
                        else
                        {
                            roles += $"{(Roles[0] == role ? "" : ", ")}{role}";
                        }
                    }
                    return roles;
                }
            }
        }
    }

    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string RoleId { get; set; } = string.Empty;
    }

    public class UserRegisterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(100, ErrorMessage = "Maximum 100 karakter adható meg")]
        public string Username { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(250, ErrorMessage = "Maximum 250 karakter adható meg")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [RegularExpression("^[a-zA-Z0-9]{6}$", ErrorMessage = Messages.WrongNeptunCode), AllowNull]
        public string NeptunCode { get; set; } = string.Empty;
        public string LdapUid { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(100, ErrorMessage = "Maximum 100 karakter adható meg")]
        public string Password { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező megadása kötelező")]
        [StringLength(100, ErrorMessage = "Maximum 100 karakter adható meg")]
        [Compare("Password", ErrorMessage = "A jelszavak nem egyeznek")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

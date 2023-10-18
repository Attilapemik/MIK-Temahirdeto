namespace PannonBlazor.Shared.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public string Email { get; set; } = string.Empty;
        public IList<Role> Roles { get; set; }

        public string LdapUid { get; set; }
        public string? NeptunCode { get; set; }

        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public int? ProgrammeId { get; set; }
        public Programme? Programme { get; set; }

        public List<Theme>? Themes { get; set; }

    }
}

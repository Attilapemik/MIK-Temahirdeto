namespace PannonBlazor.Shared.Models.Entity
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsVisibleInstructor { get; set; }
        public bool IsVisibleStudent { get; set; }
        public bool IsActive { get; set; } = false;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ThemeProgramme>? ThemeProgrammes { get; set; }
    }
}

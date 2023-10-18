namespace PannonBlazor.Shared.Models.Entity
{
    public class Programme
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; }
        public ProgrammeType ProgrammeType { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<ThemeProgramme>? ThemeProgrammes { get; set; }
        public List<User>? Users { get; set; }
    }

    public enum ProgrammeType
    {
        BSc,
        MSc
    }
}
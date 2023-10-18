using System.ComponentModel.DataAnnotations.Schema;

namespace PannonBlazor.Shared.Models.Entity
{
    public class Student
    {
        public int Id { get; set; }

        public string NeptunCode { get; set; }
        public string StudentName { get; set; }

        [ForeignKey("ProgrammeId")]
        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; }

        [ForeignKey("SemesterId")]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        [ForeignKey("ThemeId")]
        public int? ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}

using PannonBlazor.Shared.Constans;

namespace PannonBlazor.Shared.Models.Entity
{
    public class ThemeProgramme
    {
        public ThemeProgramme()
        {
        }
        public ThemeProgramme(ThemeProgramme themeProgramme, int toSemester = default)
        {//CopyTheme
            ThemeId = toSemester == default ? themeProgramme.ThemeId : default;
            ProgrammeId = themeProgramme.ProgrammeId;
            ApprovalId = toSemester == default ? themeProgramme.ApprovalId : 1;
            ApprovalDate = toSemester == default ? themeProgramme.ApprovalDate : default;
            SemesterId = toSemester == default ? themeProgramme.SemesterId : toSemester;
            DenyReason = toSemester == default ? themeProgramme.DenyReason : string.Empty;
        }

        public Theme? Theme { get; set; }
        public int ThemeId { get; set; }
        public Programme? Programme { get; set; }
        public int ProgrammeId { get; set; }
        public Approval? Approval { get; set; }
        public int ApprovalId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public Semester? Semester { get; set; }
        public int SemesterId { get; set; }
        public string DenyReason { get; set; } = string.Empty;

        public string GetTypeName()
        {
            if (Theme != null)
            {
                if (Theme.IsDual)
                {
                    return ThemeTypes.Dual;
                }
                if (Theme.IsExternal)
                {
                    return ThemeTypes.External;
                }
            }
            return ThemeTypes.Internal;
        }
    }
}

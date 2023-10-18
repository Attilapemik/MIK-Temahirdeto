namespace PannonBlazor.Shared.Models.Dto
{
    public class ThemeProgrammeFeedbackDto
    {
        public ThemeProgrammeFeedbackDto(int themeId, int programmeId, string? denyReason = default)
        {
            ThemeId = themeId;
            ProgrammeId = programmeId;
            DenyReason = denyReason ?? string.Empty;
        }

        public int ThemeId { get; set; }
        public int ProgrammeId { get; set; }
        public string DenyReason { get; set; } = string.Empty;
    }
}

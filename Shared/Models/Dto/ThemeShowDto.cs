namespace PannonBlazor.Shared.Models.Dto
{
    public class ThemeShowDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDualOrExternal { get; set; }
        public string ThemeType { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public string CreatedAt { get; set; }
        public string ExternalInstructorName { get; set; } = string.Empty;
        public string ExternalCompanyName { get; set; } = string.Empty;
        public List<ThemeProgrammeApprovalDto> Programmes { get; set; } = new List<ThemeProgrammeApprovalDto>();
        public IList<StudentDto>? Students { get; set; } = new List<StudentDto>();
    }

    public class ThemeProgrammeApprovalDto
    {
        public string ProgrammeName { get; set; }
        public bool? IsApproved { get; set; } = null; //null = Jóváhagyásra vár, true = Jóváhagyva, false = Elutasítva
        public string DenyReason { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string GetApproval
        {
            get
            {
                return IsApproved == null ? "Jóváhagyásra vár" : (IsApproved == true ? "Jóváhagyva" : "Elutasítva");
            }
        }
    }
}

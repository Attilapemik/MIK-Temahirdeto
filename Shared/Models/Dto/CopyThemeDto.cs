namespace PannonBlazor.Shared.Models.Dto
{
    public class CopyThemeDto
    {
        public int FromSemester { get; set; }
        public int ToSemester { get; set; }
        public int[] Themes { get; set; }
    }
}

namespace PannonBlazor.Shared.Constans
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string ProgrammeLeader = "Programme Leader"; //Szakfelelős
        public const string HeadOfDepartment = "Head of department"; //Tanszékvezető
        public const string Instructor = "Instructor"; //Oktató
        public const string All = Admin + "," + ProgrammeLeader + "," + HeadOfDepartment + "," + Instructor;
    }
}

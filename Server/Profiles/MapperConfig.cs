using AutoMapper;
using PannonBlazor.Shared.Constans;
using PannonBlazor.Shared.Models.Dto;
using PannonBlazor.Shared.Models.Entity;

namespace PannonBlazor.Server.Profiles
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Theme -> StudentThemeListDto
            CreateMap<Theme, StudentThemeListDto>()
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor.Username))
                .ForMember(dest => dest.Programmes,
                opt => opt.MapFrom(src => src.ThemeProgrammes))
                .ForMember(dest => dest.NeptunCode,
                opt => opt.MapFrom(src => src.StudentCode))
                .ForMember(dest => dest.ThemeType,
                opt => opt.MapFrom(src => src.IsDual ? ThemeTypes.Dual : (src.IsExternal ? ThemeTypes.External : ThemeTypes.Internal)))
                .ReverseMap();
            #endregion
            #region ThemeProgramme -> ProgrammeDto
            CreateMap<ThemeProgramme, ProgrammeDto>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Programme.Id))
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Programme.Name))
                .ForMember(dest => dest.Color,
                opt => opt.MapFrom(src => src.Programme.Color));
            #endregion
            #region Programme - ProgrammeDto
            CreateMap<Programme, ProgrammeDto>().ReverseMap();
            #endregion
            #region Programme - ProgrammeCreateDto
            CreateMap<Programme, ProgrammeCreateDto>().ReverseMap();
            #endregion
            #region Theme -> ThemeDto
            CreateMap<Theme, ThemeDto>()
                .ForMember(dest => dest.Instructor,
                opt => opt.MapFrom(src => src.Instructor == null ? "Nem található" : src.Instructor.Username))
                .ForMember(dest => dest.InstructorEmail,
                opt => opt.MapFrom(src => src.Instructor == null ? "Nem található" : src.Instructor.Email))
                .ForMember(dest => dest.SemesterName,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? "Nem található" : src.ThemeProgrammes[0].Semester == null ? "Nem található" : src.ThemeProgrammes[0].Semester.Name))
                .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => src.Status == null ? "Nem található" : src.Status.Name))
                .ForMember(dest => dest.StudentProgramme,
                opt => opt.MapFrom(src => src.StudentProgramme == null ? "Nem található" : src.StudentProgramme.Name))
                .ForMember(dest => dest.NumberOfFeedback,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? 0 : src.ThemeProgrammes.Where(tp => tp.ApprovalId != 1).Count()))
                .ForMember(dest => dest.NumberOfProgrammes,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? 0 : src.ThemeProgrammes.Count()))
                .ForMember(dest => dest.NumberOfApproved,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? 0 : src.ThemeProgrammes.Where(tp => tp.ApprovalId == 2).Count()))
                .ForMember(dest => dest.IsRejected,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? false : src.ThemeProgrammes.Any(tp => tp.ApprovalId == 3)))
                .ForMember(dest => dest.ExternalCompanyName,
                opt => opt.MapFrom(src => src.ExternalCompany == null ? "Nem található" : src.ExternalCompany.Name))
                .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt ?? DateTime.MinValue));
            #endregion
            #region Semester - SemesterGetDto
            CreateMap<Semester, SemesterGetDto>().ReverseMap();
            #endregion
            #region Semester - SemesterDto
            CreateMap<Semester, SemesterDto>().ReverseMap();
            #endregion
            #region Student -> StudentMultipleNeptunDto
            CreateMap<Student, StudentMultipleNeptunDto>()
                .ForMember(dest => dest.ThemeTitle,
                opt => opt.MapFrom(src => src.Theme.Title))
                .ForMember(dest => dest.ThemeDescription,
                opt => opt.MapFrom(src => src.Theme.Description))
                .ForMember(dest => dest.ThemeType,
                opt => opt.MapFrom(src => src.Theme.IsDual ? ThemeTypes.Dual : (src.Theme.IsExternal ? ThemeTypes.External : ThemeTypes.Internal)))
                .ForMember(dest => dest.ThemeCreatedAt,
                opt => opt.MapFrom(src => src.Theme.CreatedAt))
                .ForMember(dest => dest.InstructorEmail,
                opt => opt.MapFrom(src => src.Theme.Instructor == null ? "Nem található" : src.Theme.Instructor.Email))
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Theme.Instructor == null ? "Nem található" : src.Theme.Instructor.Username));
            #endregion
            #region User -> UserGetDto
            CreateMap<User, UserGetDto>()
                .ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles == null ? null : src.Roles.Select(x => x.DisplayName).ToList()));
            #endregion
            #region Theme -> ThemeStudentShowDto
            CreateMap<Theme, ThemeStudentShowDto>()
                .ForMember(dest => dest.NeptunCodes,
                opt => opt.MapFrom(src => src.Students == null ? null : src.Students.Select(x => x.NeptunCode).ToList()))
                .ForMember(dest => dest.Programmes,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? null : src.ThemeProgrammes.Select(x => x.Programme).ToList()))
                .ForMember(dest => dest.ThemeType,
                opt => opt.MapFrom(src => src.IsDual ? ThemeTypes.Dual : (src.IsExternal ? ThemeTypes.External : ThemeTypes.Internal)))
                .ForMember(dest => dest.NeptunCode,
                opt => opt.MapFrom(src => src.StudentCode))
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor == null ? string.Empty : src.Instructor.Username));
            #endregion
            #region Theme -> ThemeEditDto
            CreateMap<Theme, ThemeEditDto>()
                .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt == null ? string.Empty : src.CreatedAt!.ToString()))
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor == null ? string.Empty : src.Instructor.Username))
                .ForMember(dest => dest.SelectedProgrammes,
                opt => opt.MapFrom(src => src.ThemeProgrammes == null ? new List<int>() : src.ThemeProgrammes.Select(tp => tp.ProgrammeId).ToList()));
            #endregion
            #region Theme -> ThemeShowDto
            CreateMap<Theme, ThemeShowDto>()
                .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt == null ? string.Empty : src.CreatedAt!.ToString()))
                .ForMember(dest => dest.ThemeType,
                opt => opt.MapFrom(src => src.IsDual ? ThemeTypes.Dual : (src.IsExternal ? ThemeTypes.External : ThemeTypes.Internal)))
                .ForMember(dest => dest.IsDualOrExternal,
                opt => opt.MapFrom(src => src.IsDual || src.IsExternal))
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor == null ? string.Empty : src.Instructor.Username))
                .ForMember(dest => dest.Programmes,
                opt => opt.MapFrom(src => src.ThemeProgrammes));
            #endregion
            #region ThemeProgramme -> ThemeProgrammeApprovalDto
            CreateMap<ThemeProgramme, ThemeProgrammeApprovalDto>()
                .ForMember(dest => dest.IsApproved,
                opt => opt.MapFrom(src => src.ApprovalId == 1 ? null : (bool?)(src.ApprovalId == 2)));
            #endregion
            #region Student -> StudentGetDto
            CreateMap<Student, StudentDto>();
            #endregion
            #region ThemeProgramme -> ThemeProgrammeDto
            CreateMap<ThemeProgramme, ThemeProgrammeDto>()
                .ForMember(dest => dest.Approval,
                opt => opt.MapFrom(src => src.Approval == null ? "Nem található" : src.Approval.Name))
                .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Description))
                .ForMember(dest => dest.Instructor,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Instructor == null ? "Nem található" : src.Theme.Instructor.Email))
                .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Instructor == null ? "Nem található" : src.Theme.Instructor.Username))
                .ForMember(dest => dest.ProgrammeName,
                opt => opt.MapFrom(src => src.Programme == null ? "Nem található" : src.Programme.Name))
                .ForMember(dest => dest.SemesterName,
                opt => opt.MapFrom(src => src.Semester == null ? "Nem található" : src.Semester.Name))
                .ForMember(dest => dest.StatusName,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Status == null ? "Nem található" : src.Theme.Status.Name))
                .ForMember(dest => dest.Student,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Student))
                .ForMember(dest => dest.StudentCode,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.StudentCode))
                .ForMember(dest => dest.ThemeTitle,
                opt => opt.MapFrom(src => src.Theme == null ? "Nem található" : src.Theme.Title))
                .ForMember(dest => dest.ThemeType,
                opt => opt.MapFrom(src => src.GetTypeName()))
                .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.Theme != null ? src.Theme.CreatedAt ?? DateTime.MinValue : DateTime.MinValue));
            #endregion
        }
    }
}
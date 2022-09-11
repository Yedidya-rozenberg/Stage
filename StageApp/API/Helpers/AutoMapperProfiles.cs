using API.DTOs;
using API.Entities;
using AutoMapper;
using API.Extensions;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>().ForMember(dest => dest.UserName,
            opt => opt.MapFrom(src => src.Username.ToLower()));

            CreateMap<AppUser, MemberDto>()
            .ForMember(
                dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photo.Url)
            )
            .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
            );

            CreateMap<Course, CourseDto>()
            .ForMember(
                dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photo.Url)
            ).ForMember(
                dest => dest.TeacherName,
                opt => opt.MapFrom(src => src.Teacher.UserName)
            );

            CreateMap<MemberUpdateDto, AppUser>();

            CreateMap<Photo, PhotoDto>();

            CreateMap<Unit, UnitDto>();

            CreateMap<CreateUnitDto, Unit>();

            CreateMap<CourseUpdateDto, Course>();

            CreateMap<AppUser, PrivetMemberDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photo.Url)
            )
            .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
            );
        }
    }
}
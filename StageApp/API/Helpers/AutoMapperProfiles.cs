using System.Runtime.InteropServices;
using System.Linq;
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
            CreateMap<RegisterDto,AppUser>().ForMember(dest=>dest.UserName,
            opt=>opt.MapFrom(src=>src.Username.ToLower()));
        }
    }
}
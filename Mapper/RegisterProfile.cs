using AutoMapper;
using TheraGuide.Entity;
using TheraGuide.ViewModels;

namespace TheraGuide.Mapper
{
    public class RegisterProfile :Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}

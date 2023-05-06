using API.Models;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>().ForMember(dest => dest.CreatedOn, cfg => cfg.MapFrom(srcs => DateTime.Today));
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Birthday,
                cfg => cfg.MapFrom((src, dest) =>
                    src.IsFieldPresent(nameof(dest.Birthday)) ? src.Birthday : dest.Birthday))
            .ForMember(dest => dest.Name,
                cfg => cfg.MapFrom((src, dest) =>
                    src.IsFieldPresent(nameof(dest.Name)) ? src.Name : dest.Name))
            .ForMember(dest => dest.Gender,
                cfg => cfg.MapFrom((src, dest) =>
                    src.IsFieldPresent(nameof(dest.Gender)) ? src.Gender : dest.Gender))
            .ForMember(dest => dest.ModifiedOn, cfg => cfg.MapFrom(src => DateTime.Today));

        CreateMap<UpdateLoginDto, User>()
            .ForMember(dest => dest.ModifiedOn, cfg => cfg.MapFrom(src => DateTime.Today))
            .ForMember(dest => dest.Login, cfg => cfg.MapFrom(src => src.NewLogin));

        CreateMap<UpdatePasswordDto, User>()
            .ForMember(dest => dest.ModifiedOn, cfg => cfg.MapFrom(src => DateTime.Today))
            .ForMember(dest => dest.Password, cfg => cfg.MapFrom(src => src.Password));

        CreateMap<User, UserDto>().ForMember(dest => dest.Active, cgf => cgf.MapFrom(src => src.RevokedOn == null));
    }
}
using _RoBotland.Models;
using AutoMapper;

namespace _RoBotland
{
    public class MapperProfile: Profile
    { 
        public MapperProfile() {
            CreateMap<ProductDto, Product>();
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserRegisterDto, UserDetails>().ForMember(dest => dest.HomeAdress, opt => opt.MapFrom(x=>x.City+" "+x.ZipCode+"/n"+x.Street+" "+x.HouseNumber));
            CreateMap<User, UserDto>();
        }
    }
}

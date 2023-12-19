using _RoBotland.Models;
using AutoMapper;

namespace _RoBotland
{
    public class MapperProfile: Profile
    { 
        public MapperProfile() {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserRegisterDto, UserDetails>()
                .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(x=>x.City+" "+x.ZipCode+"/n"+x.Street+" "+x.HouseNumber));
            CreateMap<User, UserDto>();
            CreateMap<ShoppingCartItem, OrderDetails>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.Product.Id))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(x => x.Quantity));
            CreateMap<UserDetailsDto, UserDetails>()
               .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(x => x.City + " " + x.ZipCode + "/n" + x.Street + " " + x.HouseNumber));

        }
    }
}

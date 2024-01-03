using _RoBotland.Models;
using AutoMapper;

namespace _RoBotland
{
    public class MapperProfile: Profile
    { 
        public MapperProfile() {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserRegisterDto, UserDetails>()
                .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(x=>x.City+" "+x.ZipCode+" \n"+x.Street+" "+x.HouseNumber));
            CreateMap<User, UserDto>();
            CreateMap<ShoppingCartItem, OrderDetails>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(x => x.Product.Id))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
            CreateMap<UserDetailsDto, UserDetails>()
               .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(x => x.City + " " + x.ZipCode + " \n" + x.Street + " " + x.HouseNumber));
            CreateMap<UserDetails, UserDetailsDto>()//
                .ForMember(dest => dest.City, opt => opt.MapFrom(x => x.HomeAddress.Split(" ", StringSplitOptions.None)[0] ?? " "))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(x => x.HomeAddress.Split(" ", StringSplitOptions.None)[1] ?? " "))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(x => x.HomeAddress.Split(" ",StringSplitOptions.None)[2].Replace("\n", "") ?? " "))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(x => x.HomeAddress.Split(" ",StringSplitOptions.None)[3] ?? " "));
            CreateMap<Order, OrderDto>();
        }
    }
}

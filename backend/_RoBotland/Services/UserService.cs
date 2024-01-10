using _RoBotland.Enums;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace _RoBotland.Services
{
    public class UserService : IUserService
    {
        private DataContext _dataContext;
        private IMapper _mapper;
        private IConfiguration _config;

        public UserService(DataContext dataContext, IMapper mapper, IConfiguration configuration)
        {
            this._dataContext = dataContext;
            this._mapper = mapper;
            this._config = configuration;
        }

        public UserLoginResponseDto Login(UserLoginDto request)
        {
            var user = _dataContext.Users.FirstOrDefault(x=>x.Username == request.Username)??throw new Exception("Not Found");
            if (!(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) && user.Username == request.Username)) 
                throw new Exception("Bad Password");
            var userd = _mapper.Map<UserDto>(user);
            string token = GenerateToken(userd);
            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto(token);
            return userLoginResponseDto;
            
        }

        public UserDto Register(UserRegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var userDetails = _mapper.Map<UserDetails>(request);
            var user= _mapper.Map<User>(request);
            if (_dataContext.Users.FirstOrDefault(x => x.Username == request.Username) != null || _dataContext.UserDetails.FirstOrDefault(x=>x.Email==request.Email)!=null)
                throw new Exception("User with current username or current email already exists");
            user.PasswordHash = passwordHash;
            user.UserDetails = userDetails;
            user.Role= Role.USER;
            _dataContext.Users.Add(user);
            _dataContext.UserDetails.Add(userDetails);
            _dataContext.SaveChanges();
            var response=_mapper.Map<UserDto>(user);
            return response;
         }
        public List<OrderDto> GetHistory(string username)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();
            var history = _dataContext.Orders.Where(x => x.UserDetails.Id == user.Id).ToList();
            var userD = _dataContext.UserDetails.FirstOrDefault(x => x.Id == user.Id);
            List<OrderDto> orders = new List<OrderDto>();
            foreach (var order in history)
            {
                var detail = _mapper.Map<OrderDto>(order);
                var products = _dataContext.OrderDetails.Where(x => x.OrderId == order.Id).ToList();
                List<ShoppingCartItem> items = new List<ShoppingCartItem>();
                foreach (var product in products)
                {
                    var prod=_dataContext.Products.Find(product.ProductId) ?? throw new Exception();
                    ShoppingCartItem item = new ShoppingCartItem(items.Count, _mapper.Map<ProductDto>(prod), product.Quantity, product.Total);
                    items.Add(item);
                }
                detail.Items = items;
                orders.Add(detail);
            }
            //i assume that user cannot change his userdetails between orders
            return orders;
        }
        private string GenerateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public float GetAccountBalance(string username)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();
            return user != null ? user.AccountBalance : 0.0f;
        }
        private void UpdateAccountBalanceUser(User user)
        {
            var existingUser = _dataContext.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.AccountBalance = user.AccountBalance;
                _dataContext.SaveChanges();
            }
        }
        private User GetUserById(Guid id)
        {
            var user = _dataContext.Users.Find(id);
            return user;
        }

        public bool DepositToAccount(string username, float amount)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();
            if (user != null)
            {
                user.AccountBalance += amount;
                UpdateAccountBalanceUser(user);
                return true;
            }
            return false;
        }

        public bool WithdrawFromAccount(string username, float amount)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();
            if (user != null && user.AccountBalance >= amount)
            {
                user.AccountBalance -= amount;
                UpdateAccountBalanceUser(user);
                return true;
            }
            return false;
        }
    }
}

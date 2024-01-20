using _RoBotland.Enums;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
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
            if (!(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))) 
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
            if (_dataContext.Users
                .Include(x=>x.UserDetails)
                .FirstOrDefault(x => x.Username == request.Username||x.UserDetails.Email==request.Email) != null)
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
            var history = _dataContext.Orders
                .Include(x=>x.OrderDetails)
                .ThenInclude(x=>x.Product)
                .Include(x=>x.UserDetails)
                .ThenInclude(x=>x.User)
                .Where(x=>x.UserDetails.User.Username==username).ToList();
            List<OrderDto> orders = new List<OrderDto>();
            
            foreach (var order in history)
            {
                var detail = _mapper.Map<OrderDto>(order);
                detail.Items = new List<ShoppingCartItem>();
                foreach (var position in order.OrderDetails)
                {
                    ShoppingCartItem item = new ShoppingCartItem(detail.Items.Count, _mapper.Map<ProductDto>(position.Product), position.Quantity, position.Total);
                    detail.Items.Add(item);
                }
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
        public UserInfoDto GetUserInfo(string username)
        {
            var user = _mapper.Map<UserInfoDto>(_dataContext.Users.Where(x=>x.Username==username).Include(x=>x.UserDetails).First());
            return user;
        }

        public bool DepositToAccount(string username, float amount)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();   
            user.AccountBalance += amount;
            UpdateAccountBalanceUser(user);
            return true;
        }

        public bool WithdrawFromAccount(string username, float amount)
        {
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception();
            if (user.AccountBalance >= amount)
            {
                user.AccountBalance -= amount;
                UpdateAccountBalanceUser(user);
                return true;
            }
            return false;
        }
    }
}

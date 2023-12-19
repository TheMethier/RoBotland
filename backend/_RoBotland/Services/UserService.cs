using _RoBotland.Enums;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public string Login(UserLoginDto request)
        {
            var user = _dataContext.Users.FirstOrDefault(x=>x.Username == request.Username);           
            if (user is null)
                throw new Exception("Not Found");
            if (!(BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) && user.Username == request.Username)) 
                throw new Exception("Bad Password");
            var response = _mapper.Map<UserDto>(user);
            var token = GenerateToken(response);
            return token;
        }

        public UserDto Register(UserRegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var userDetails = _mapper.Map<UserDetails>(request);
            var user= _mapper.Map<User>(request);
            user.PasswordHash = passwordHash;
            user.UserDetails = userDetails;
            user.Role= Role.USER;
            _dataContext.Users.Add(user);
            _dataContext.UserDetails.Add(userDetails);
            _dataContext.SaveChanges();
            var response=_mapper.Map<UserDto>(user);
            return response;
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
        public bool WithdrawFromAccount(Guid userId, float amount)
        {
            var user = GetUserById(userId);

            if (user != null && user.AccountBalance >= amount)
            {
                user.AccountBalance -= amount;
                UpdateAccountBalanceUser(user);
                return true;
            }
            return false;
        }
        public void DepositToAccount(Guid userId, float amount)
        {
            var user = GetUserById(userId);

            if (user != null)
            {
                user.AccountBalance += amount;
                UpdateAccountBalanceUser(user);
            }
        }
        public float GetAccountBalance(Guid userId)
        {
            var user = GetUserById(userId);
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
    }
}

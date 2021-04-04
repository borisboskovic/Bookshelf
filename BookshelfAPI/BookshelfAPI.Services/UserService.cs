using BookshelfAPI.Data;
using BookshelfAPI.Data.Helpers;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<BookshelfUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly BookshelfDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookshelfUser User
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return _context.Users.FirstOrDefault(user => user.Id == userId); ;
            }
        }

        public UserService(
            UserManager<BookshelfUser> userManager,
            IConfiguration configuration,
            BookshelfDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        public void EditUser()
        {
            throw new NotImplementedException();
        }



        public async Task<int> RegisterAsync(UserRegisterDto model)
        {
            //TODO: Tabela sa istorijom lozinki
            var user = new BookshelfUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                EmailConfirmed = false,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth,
                RegistrationTime = DateTime.Now,
                Active = true,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //TODO: Send confirmation Email. On login check not confirmed email error
                await _userManager.AddToRoleAsync(user, "Reader");
                return LocalizationCodes.Success;
            }
            else
            {   //TODO: Provjeriti koje su moguće greške i vratiti odgovarajuće kodove
                return LocalizationCodes.RegisterFail_Default;
            }
        }



        public async Task<AuthenticationResultDto> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResultDto
                {
                    StatusCode = LocalizationCodes.LoginFail_UserNotFound
                };
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordCorrect)
            {
                return new AuthenticationResultDto
                {
                    StatusCode = LocalizationCodes.LoginFail_WrongPassword
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var issuer = _configuration["TokenConstants:Issuer"];
            var audience = _configuration["TokenConstants:Audience"];
            var secret = _configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("name", $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", roles[0]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signinCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signinCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResultDto
            {
                StatusCode = LocalizationCodes.Success,
                TokenJson = tokenJson
            };
        }
    }
}

using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs;
using BookshelfAPI.Services.Helpers;
using BookshelfAPI.Services.RequestModels.User;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IStringLocalizer<UserService> _localizer;

        public BookshelfUser User
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return _context.Users.FirstOrDefault(user => user.Id == userId);
            }
        }

        public UserService(
            UserManager<BookshelfUser> userManager,
            IConfiguration configuration,
            BookshelfDbContext context,
            IHttpContextAccessor httpContextAccessor,
            EmailConfiguration emailConfiguration,
            IStringLocalizer<UserService> localizer
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _emailConfiguration = emailConfiguration;
            _localizer = localizer;
        }

        public async Task<ServiceResponse> RegisterAsync(Register_RequestModel model)
        {
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

            var response = new ServiceResponse();
            if (result.Succeeded)
            {
                _ = SendConfirmationEmailAsync(user);
                await _userManager.AddToRoleAsync(user, "Reader");
                response.Succeeded = true;
                return response;
            }
            else
            {   //TODO: Provjeriti koje su moguće greške i vratiti odgovarajuće kodove
                foreach(var error in result.Errors)
                {
                    response.Errors.Add(error.Code, new string[] { error.Description });
                }
                response.Succeeded = false;
                return response;
            }
        }

        public async Task<ServiceResponse> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var response = new ServiceResponse();
            if (user == null)
            {
                //TODO: Localize
                response.Errors.Add("Login failed", new string[] { "Wrong credentials." });
                return response;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordCorrect)
            {
                //TODO: Localize
                response.Errors.Add("Login failed", new string[] { "Wrong credentials." });
                return response;
            }

            if (!user.EmailConfirmed)
            {
                _ = SendConfirmationEmailAsync(user);
                //TODO: Localize
                response.Errors.Add(
                    "LoginFailed",
                    new string[] { "Email is not confirmed. Confirmation email has been sent." }
                );
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var issuer = _configuration["TokenConstants:Issuer"];
            var audience = _configuration["TokenConstants:Audience"];
            var secret = _configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("name", $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };
            roles.ToList().ForEach(role => claims.Add(new Claim("roles", role)));

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

            response.Succeeded = true;
            response.Body = new AuthenticationResultDto
            {
                Token = tokenJson
            };
            return response;
        }

        private async Task<ServiceResponse> SendConfirmationEmailAsync(BookshelfUser user)
        {
            var response = new ServiceResponse();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, false);
                    //Use code bellow instead if authentication is required
                    //await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                    var baseURL = _configuration["TokenConstants:Audience"];
                    var message = EmailMessageHelper
                        .CreateEmailConfirmationMessage(_emailConfiguration.From, user.Email, token, baseURL, _localizer);

                    await client.SendAsync(message);
                }
                catch
                {
                    //TODO: Log exception
                    return response;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            response.Succeeded = true;
            return response;
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var response = new ServiceResponse();
            if (user == null)
            {
                response.Errors.Add(
                    "UserNotFound",
                    new string[] { "We were not able to find a user with that email address" }
                );
                return response;
            }
            if (user.EmailConfirmed)
            {
                response.Errors.Add(
                    "AlreadyConfirmed",
                    new string[] { "That email address is already confirmed" }
                );
                return response;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                response.Succeeded = true;
            }
            return response;
        }

        public void EditUser()
        {
            throw new NotImplementedException();
        }
    }
}

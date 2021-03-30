using BookshelfAPI.Data.Helpers;
using BookshelfAPI.Services;
using BookshelfAPI.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }



        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok("Index");
        }



        [HttpGet("Secret")]
        [Authorize]
        public IActionResult Secret()
        {
            return Ok("Secret");
        }



        [HttpGet("Authenticate")]
        public IActionResult Authenticate()
        {
            var issuer = _configuration["TokenConstants:Issuer"];
            var audience = _configuration["TokenConstants:Audience"];
            var secret = _configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
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

            return Ok(new { access_token = tokenJson });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);
            return (result == LocalizationCodes.Success) ? Ok() : BadRequest(result);
        }
    }
}

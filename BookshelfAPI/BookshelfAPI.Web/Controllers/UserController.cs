﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookshelfAPI.Web.Controllers
{
    [Route("/api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}

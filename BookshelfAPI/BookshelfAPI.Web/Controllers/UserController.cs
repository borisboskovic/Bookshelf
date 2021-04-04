using BookshelfAPI.Data.Helpers;
using BookshelfAPI.Services;
using BookshelfAPI.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    [Authorize(Roles = "Reader")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("Secret")]
        public IActionResult Secret()
        {
            return Ok(_userService.User);
        }



        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] object requestBody)
        {
            var requestModel = new
            {
                email = "",
                password = ""
            };

            requestModel = JsonConvert.DeserializeAnonymousType(requestBody.ToString(), requestModel);

            var authenticationResult = await _userService.AuthenticateAsync(requestModel.email, requestModel.password);

            return authenticationResult.StatusCode == LocalizationCodes.Success ?
                Ok(new { access_token = authenticationResult.TokenJson, issued_at = DateTime.Now }) :
                BadRequest(authenticationResult.StatusCode);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);

            return (result == LocalizationCodes.Success) ? Ok() : BadRequest(result);
        }
    }
}

using BookshelfAPI.Services;
using BookshelfAPI.Services.RequestModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/[Controller]/[Action]")]
    [Authorize(Roles = "Reader")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet("Secret")]
        public IActionResult Secret()
        {
            return Ok(_userService.User);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Authenticatie_RequestModel requestModel)
        {
            var result = await _userService.AuthenticateAsync(requestModel.Email, requestModel.Password);

            return result.Succeeded
                ? Ok(result.Body)
                : BadRequest(result);
        }

        //TODO: Add validation
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register_RequestModel model)
        {
            var result = await _userService.RegisterAsync(model);

            return (result.Succeeded) ? Ok() : BadRequest(result);
        }

        //TODO: Da li je zaista potrebna lozinka ???
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] object requestBody)
        {
            var requestModel = new
            {
                Email = "",
                Password = "",
                Token = ""
            };
            requestModel = JsonConvert.DeserializeAnonymousType(requestBody.ToString(), requestModel);

            var result = await _userService.ConfirmEmailAsync(requestModel.Email, requestModel.Token);
            return (result.Succeeded) ? Ok(result) : BadRequest(result);
        }



        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] object requestModel)
        {
            var model = new
            {
                Email = ""
            };
            model = JsonConvert.DeserializeAnonymousType(requestModel.ToString(), model);

            return Ok(model.Email);
        }
    }
}

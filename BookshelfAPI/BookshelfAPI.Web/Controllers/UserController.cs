using BookshelfAPI.Services;
using BookshelfAPI.Services.RequestModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/[Controller]/[Action]")]
    [Authorize(Roles = "reader")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Secret()
        {
            return Ok(_userService.User);
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var result = await _userService.GetUserInfo();
            
            return result.Succeeded
                ? Ok(result.Body)
                : BadRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Authenticatie_RequestModel model)
        {
            var result = await _userService.AuthenticateAsync(model.Email, model.Password);

            return result.Succeeded
                ? Ok(result.Body)
                : BadRequest(result);
        }

        //TODO: Add validation
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] Register_RequestModel model)
        {
            var result = await _userService.RegisterAsync(model);
            return (result.Succeeded) ? Ok() : BadRequest(result);
        }

        //TODO: Da li je zaista potrebna lozinka ???
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmail_RequestModel model)
        {
            var result = await _userService.ConfirmEmailAsync(model.Email, model.Token);
            return (result.Succeeded) ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SendResetPasswordEmail(SendResetPasswordEmail_RequestModel model)
        {
            return Ok(model.Email);
        }
    }
}

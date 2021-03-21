using BookshelfAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [Route("/api/[Controller]")]
    public class UserController : ControllerBase
    {
        public UserController() { }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok("Index");
        }
    }
}

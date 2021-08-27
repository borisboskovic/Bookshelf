using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/Admin/[Controller]/[Action]")]
    [Authorize(Roles = "Administrator")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            var result = await _roleService.CreateRole(name);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> MakeAdmin([FromQuery] string userId)
        {
            var result = await _roleService.MakeAdmin(userId);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> RemoveAdmin([FromQuery] string userId)
        {
            var result = await _roleService.RemoveAdmin(userId);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}

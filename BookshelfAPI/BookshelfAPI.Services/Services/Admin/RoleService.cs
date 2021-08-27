using BookshelfAPI.Services.Interfaces.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Services.Admin
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse> CreateRole(string roleName)
        {
            var response = new ServiceResponse();

            var role = new IdentityRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                response.Succeeded = true;
                return response;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(error.Code, new string[] { error.Description });
                }
                return response;
            }
        }

        public Task<ServiceResponse> MakeAdmin(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> RemoveAdmin(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

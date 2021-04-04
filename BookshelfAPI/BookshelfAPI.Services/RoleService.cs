using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }



        public async Task CreateRoleAsync(string name)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            });
        }
    }
}

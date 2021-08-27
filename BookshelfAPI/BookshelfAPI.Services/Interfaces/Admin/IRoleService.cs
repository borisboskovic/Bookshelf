using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services.Interfaces.Admin
{
    public interface IRoleService
    {
        public Task<ServiceResponse> CreateRole(string roleName);
        public Task<ServiceResponse> MakeAdmin(string userId);
        public Task<ServiceResponse> RemoveAdmin(string userId);
    }
}

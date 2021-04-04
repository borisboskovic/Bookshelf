using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public interface IRoleService
    {
        public Task CreateRoleAsync(string name);
    }
}

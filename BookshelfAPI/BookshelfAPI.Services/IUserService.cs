using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public interface IUserService
    {
        public BookshelfUser User { get; }
        public Task<int> RegisterAsync(UserRegisterDto model);
        public void EditUser();
        public Task<AuthenticationResultDto> AuthenticateAsync(string email, string password);
    }
}

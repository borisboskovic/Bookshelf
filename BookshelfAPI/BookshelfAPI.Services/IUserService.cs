using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public interface IUserService
    {
        public BookshelfUser User { get; }
        public Task<int> RegisterAsync(UserRegisterDto model);
        public Task<AuthenticationResultDto> AuthenticateAsync(string email, string password);
        public Task<int> SendConfirmationEmailAsync(string email, string password);
        public Task<int> ConfirmEmailAsync(string email, string password, string token);
        public void EditUser();
    }
}

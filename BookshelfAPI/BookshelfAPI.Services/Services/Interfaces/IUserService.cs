using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.RequestModels.User;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public interface IUserService
    {
        public BookshelfUser User { get; }
        public Task<ServiceResponse> RegisterAsync(Register_RequestModel model);
        public Task<ServiceResponse> AuthenticateAsync(string email, string password);
        public Task<ServiceResponse> ConfirmEmailAsync(string email, string token);
        public void EditUser();
    }
}

using BookshelfAPI.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public interface IUserService
    {
        public Task<int> RegisterAsync(UserRegisterDto model);
        public void EditUser();
    }
}

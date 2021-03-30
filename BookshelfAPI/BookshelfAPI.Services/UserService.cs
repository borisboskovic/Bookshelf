using BookshelfAPI.Data;
using BookshelfAPI.Data.Helpers;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BookshelfAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<BookshelfUser> _userManager;
        private readonly BookshelfDbContext _context;

        public UserService(UserManager<BookshelfUser> userManager, BookshelfDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void EditUser()
        {
            throw new NotImplementedException();
        }

        public async Task<int> RegisterAsync(UserRegisterDto model)
        {
			//TODO: Tabela sa istorijom lozinki
            var user = new BookshelfUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.LastName,
                DateOfBirth = model.DateOfBirth,
                RegistrationTime = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return LocalizationCodes.Success;
            }else
            {   //TODO: Provjeriti koje su moguće greške i vratiti odgovarajuće kodove
                return LocalizationCodes.RegisterFail_Default;
            }
        }
    }
}

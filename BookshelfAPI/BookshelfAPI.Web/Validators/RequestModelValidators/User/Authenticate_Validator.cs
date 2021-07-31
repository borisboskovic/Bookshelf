using BookshelfAPI.Services.RequestModels.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshelfAPI.Web.Validators.RequestModelValidators.User
{
    public class Authenticate_Validator : AbstractValidator<Authenticatie_RequestModel>
    {
        public Authenticate_Validator()
        {
            RuleFor(e => e.Email).EmailAddress();

            RuleFor(e => e.Password).MinimumLength(8);
        }
    }
}

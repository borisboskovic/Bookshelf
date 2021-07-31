using BookshelfAPI.Services.RequestModels.User;
using FluentValidation;

namespace BookshelfAPI.Web.Validators.RequestModelValidators.User
{
    public class Register_Validator: AbstractValidator<Register_RequestModel>
    {
        public Register_Validator()
        {
            RuleFor(e => e.Email).EmailAddress();

            RuleFor(e => e.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 256)
                .Must(ValidationRules.ContainOnlyLetters)
                .WithMessage("{PropertyName} must contain only letters");
            
            RuleFor(e => e.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 256)
                .Must(ValidationRules.ContainOnlyLetters)
                .WithMessage("{PropertyName} must contain only letters");

            RuleFor(e => e.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}

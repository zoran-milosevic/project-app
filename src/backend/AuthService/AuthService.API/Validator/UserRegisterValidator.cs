using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterBindingModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress();

            RuleFor(m => m.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Password)
                .NotEmpty()
                .WithMessage("Please enter the password");

            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.Password)
                .When(m => !string.IsNullOrEmpty(m.Password))
                .WithMessage("The password and confirmation password does not match.");
        }
    }
}
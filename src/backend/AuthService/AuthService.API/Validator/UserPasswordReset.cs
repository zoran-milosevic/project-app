using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class UserPasswordResetValidator : AbstractValidator<UserPasswordResetBindingModel>
    {
        public UserPasswordResetValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(m => m.Token)
                .NotEmpty();

            RuleFor(m => m.Password)
                .NotEmpty();

            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.Password)
                .When(m => !string.IsNullOrEmpty(m.Password))
                .WithMessage("The password and confirmation password does not match.");
        }
    }
}
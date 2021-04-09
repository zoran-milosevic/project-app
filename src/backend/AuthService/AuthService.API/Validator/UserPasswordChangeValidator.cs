using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class UserPasswordChangeValidator : AbstractValidator<UserPasswordChangeBindingModel>
    {
        public UserPasswordChangeValidator()
        {
            RuleFor(m => m.OldPassword)
                .NotEmpty();

            RuleFor(m => m.NewPassword)
                .NotEmpty();

            RuleFor(m => m.ConfirmPassword)
                .NotEmpty();

            RuleFor(m => m.NewPassword)
                .NotEmpty();

            RuleFor(m => m.ConfirmPassword)
                .Equal(m => m.NewPassword)
                .When(m => !string.IsNullOrEmpty(m.NewPassword))
                .WithMessage("The password and confirmation password does not match.");
        }
    }
}
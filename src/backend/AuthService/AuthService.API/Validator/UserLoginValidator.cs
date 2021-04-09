using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginBindingModel>
    {
        public UserLoginValidator()
        {
            RuleFor(m => m.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(m => m.Password)
                .NotEmpty();
        }
    }
}
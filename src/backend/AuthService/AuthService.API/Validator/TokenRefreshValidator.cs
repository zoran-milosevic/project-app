using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class TokenRefreshValidator : AbstractValidator<TokenRefreshBindingModel>
    {
        public TokenRefreshValidator()
        {
            RuleFor(m => m.OldToken)
                .NotEmpty();
        }
    }
}
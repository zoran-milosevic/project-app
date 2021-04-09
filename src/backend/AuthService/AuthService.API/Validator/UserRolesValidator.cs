using FluentValidation;
using AuthService.Model.Binding;

namespace AuthService.Api.Validations
{
    public class UserRolesValidator : AbstractValidator<UserRolesBindingModel>
    {
        public UserRolesValidator()
        {
            RuleFor(m => m.UserId)
                .NotEmpty();

            RuleForEach(m => m.RoleNames)
                .NotNull();
        }
    }
}
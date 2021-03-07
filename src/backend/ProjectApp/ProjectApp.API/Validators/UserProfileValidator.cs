using FluentValidation;
using ProjectApp.Model.Binding;

namespace ProjectApp.Api.Validations
{
    public class UserProfileValidator : AbstractValidator<UserProfileBindingModel>
    {
        public UserProfileValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.UserId)
                .NotEmpty()
                .WithMessage("'UserId' must not be 0.");
        }
    }
}
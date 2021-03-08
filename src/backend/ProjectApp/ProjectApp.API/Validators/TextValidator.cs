using FluentValidation;
using ProjectApp.BindingModel;

namespace ProjectApp.Api.Validations
{
    public class TextValidator : AbstractValidator<TextBindingModel>
    {
        public TextValidator()
        {
            RuleFor(m => m.Text)
                .NotEmpty();
        }
    }
}
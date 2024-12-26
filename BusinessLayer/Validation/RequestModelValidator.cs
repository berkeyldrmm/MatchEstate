using DTOLayer;
using FluentValidation;

namespace BusinessLayer.Validation
{
    public class RequestModelValidator : AbstractValidator<RequestModelDTO>
    {
        public RequestModelValidator()
        {
            RuleFor(t => t.ClientNameSurname).NotEmpty().When(i => i.RadioForClient == "1").WithMessage("Please enter client name surname.");
            RuleFor(t => t.ClientEmail)
                .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(t => t.ClientPhoneNumber)
                .NotEmpty().When(i => i.RadioForClient == "1").WithMessage("Please enter client phone number.")
                .Length(10).WithMessage("Please enter a valid phone number (10 digits).");

            RuleFor(t => t.MaxPrice).NotEmpty().WithMessage("Please enter a valid maximum price information.")
                .GreaterThan(0).WithMessage("Please enter a valid price range.");
            RuleFor(t => t.MinPrice).LessThan(t => t.MaxPrice).WithMessage("Please enter a valid price range.");
            RuleFor(t => t.City).Must(c => c != "0").WithMessage("Please select a city");
            RuleFor(t => t.Details).MaximumLength(1000).WithMessage("Details field is too long.");
        }
    }
}

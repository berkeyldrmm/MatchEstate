using DTOLayer;
using FluentValidation;

namespace BusinessLayer.Validation
{
    public class ListingModelValidator : AbstractValidator<AddListingDTO>
    {
        public ListingModelValidator()
        {
            RuleFor(l => l.ClientNameSurname).NotEmpty().When(l => l.RadioForClient == "1").WithMessage("Please enter client name surname.");
            RuleFor(l => l.ClientEmail).EmailAddress().When(l => l.RadioForClient == "1").WithMessage("Please enter a valid email address.");
            RuleFor(l => l.ClientPhoneNumber)
                .NotEmpty().When(l => l.RadioForClient == "1").WithMessage("Please enter client phone number.")
                .Length(10).When(l => l.RadioForClient == "1").WithMessage("Please enter a valid phone number (10 digits).");

            RuleFor(l => l.ListingTitle).NotEmpty().WithMessage("Please enter a title for the listing.");
            RuleFor(t => t.City).Must(c => c != "0").WithMessage("Please select a city");
            RuleFor(l => l.Price).NotNull().GreaterThan(0).WithMessage("Please enter a valid price.");

            RuleFor(l => l.SquareMetersize)
                .NotNull().WithMessage("Please enter a valid square metersize.");
            RuleFor(l => l.ParselNumber)
                .NotNull().WithMessage("Please enter a valid parsel number.");
            RuleFor(l => l.BlockNumber)
                .NotNull().WithMessage("Please enter a valid sheet number.");

            RuleFor(l => l.SheetNumber)
                .NotNull().When(l => l.PropertyType == "2" || l.PropertyType == "4").WithMessage("Please enter a valid block number.");
            RuleFor(l => l.AgeOfBuilding)
                .NotNull().When(l => l.PropertyType == "1" || l.PropertyType == "5").WithMessage("Please enter age of building.");
            RuleFor(l => l.Floor)
                .NotNull().When(l => l.PropertyType == "1" || l.PropertyType == "5").WithMessage("Please enter floor information.");
            RuleFor(l => l.NumberOfFloor)
                .NotNull().When(l => l.PropertyType == "1" || l.PropertyType == "5").WithMessage("Please enter number of floor information.");
            RuleFor(l => l.NumberOfBathrooms)
                .NotNull().When(l => l.PropertyType == "5").WithMessage("Please enter number of bathrooms.");
            RuleFor(l => l.NumberOfBalcony)
                .NotNull().When(l => l.PropertyType == "5").WithMessage("Please enter number of balcony.");
            RuleFor(l => l.Dues)
                .NotNull().When(l => l.PropertyType == "1" || l.PropertyType == "5").WithMessage("Please enter dues.");

            RuleFor(l => l.Details).MaximumLength(1000).WithMessage("Details field is too long.");
        }
    }
}
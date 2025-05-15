using DTOLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.Validation;

public class UpdateListingDtoValidator : AbstractValidator<UpdateListingDto>
{
    public UpdateListingDtoValidator()
    {
        RuleFor(l => l.ListingTitle).NotEmpty().WithMessage("Please enter a title for the listing.");
    }
}

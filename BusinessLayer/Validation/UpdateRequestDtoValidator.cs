using DTOLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.Validation;

public class UpdateRequestDtoValidator : AbstractValidator<UpdateRequestDto>
{
    public UpdateRequestDtoValidator()
    {
        RuleFor(l => l.RequestTitle).NotEmpty().WithMessage("Please enter a title for the request.");
    }
}

using DataAccessLayer.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Shared.Dtos.PropertyListing;

namespace BusinessLayer.Validation;

public class UpdateListingDtoValidator : AbstractValidator<UpdateListingDto>
{
    public IClientRepository _clientRepository { get; set; }
    public IHttpContextAccessor _httpContextAccessor { get; set; }
    public UpdateListingDtoValidator(IClientRepository clientRepository, IHttpContextAccessor httpContextAccessor)
    {
        _clientRepository = clientRepository;
        _httpContextAccessor = httpContextAccessor;

        RuleFor(l => l.ClientNameSurname).NotEmpty().When(l => l.RadioForClient == "1").WithMessage("Please enter client name surname");
        RuleFor(l => l.ClientEmail).EmailAddress().When(l => l.RadioForClient == "1").WithMessage("Please enter a valid email address");
        RuleFor(l => l.ClientPhoneNumber)
            .NotEmpty().When(l => l.RadioForClient == "1").WithMessage("Please enter client phone number")
            .Length(10).When(l => l.RadioForClient == "1").WithMessage("Please enter a valid phone number (10 digits)")
            .MustAsync(CheckPhoneNumberExists).When(l => l.RadioForClient == "1").WithMessage("There is an user with the same phone number");

        RuleFor(l => l.ListingTitle).NotEmpty().WithMessage("Please enter a title for the listing");
        RuleFor(t => t.City).Must(c => c != "0").WithMessage("Please select a city");
        RuleFor(l => Convert.ToDecimal(l.Price)).NotNull().GreaterThan(0).WithMessage("Please enter a valid price");

        RuleFor(l => l.Details).MaximumLength(1000).WithMessage("Details field is too long");
    }

    public async Task<bool> CheckPhoneNumberExists(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _clientRepository.ControlUserPhoneNumber(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value, phoneNumber);
    }
}

using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOLayer.Dtos;
using EntityLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusinessLayer.Validation
{
    public class AddRequestDtoValidator : AbstractValidator<AddRequestDto>
    {
        public IClientRepository _clientRepository { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public AddRequestDtoValidator(IClientRepository clientRepository, IHttpContextAccessor httpContextAccessor)
        {
            _clientRepository = clientRepository;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(t => t.ClientNameSurname).NotEmpty().When(i => i.RadioForClient == "1").WithMessage("Please enter client name surname");
            RuleFor(t => t.ClientEmail)
                .EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(t => t.ClientPhoneNumber)
                .NotEmpty().When(i => i.RadioForClient == "1").WithMessage("Please enter client phone number")
                .Length(10).WithMessage("Please enter a valid phone number (10 digits)")
                .MustAsync(CheckPhoneNumberExists).When(l => l.RadioForClient == "1").WithMessage("There is an user with the same phone number.");

            RuleFor(t => t.MaxPrice).NotEmpty().WithMessage("Please enter a valid maximum price information")
                .GreaterThan(0).WithMessage("Please enter a valid price range");
            RuleFor(t => t.MinPrice).LessThan(t => t.MaxPrice).WithMessage("Please enter a valid price range");
            RuleFor(t => t.City).Must(c => c != "0").WithMessage("Please select a city");
            RuleFor(t => t.Details).MaximumLength(1000).WithMessage("Details field is too long");
        }
        public async Task<bool> CheckPhoneNumberExists(string phoneNumber, CancellationToken cancellationToken)
        {
            return await _clientRepository.ControlUserPhoneNumber(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value, phoneNumber);
        }
    }
}

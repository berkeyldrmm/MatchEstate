using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Auth;

namespace BusinessLayer.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.UsernameOrMail).NotEmpty().WithMessage("Please enter your username or email address");
            RuleFor(l => l.UsernameOrMail).EmailAddress().WithMessage("Please enter a valid username or email address");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Please enter your password");
        }
    }
}

using DTOLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class LoginValidation : AbstractValidator<LoginModelDTO>
    {
        public LoginValidation()
        {
            RuleFor(l => l.Mail).NotEmpty().WithMessage("Please enter your email address.");
            RuleFor(l => l.Mail).EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(l => l.Password).NotEmpty().WithMessage("Please enter your password.");
        }
    }
}

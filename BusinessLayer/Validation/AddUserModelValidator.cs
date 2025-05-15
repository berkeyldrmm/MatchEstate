using DTOLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class AddUserModelValidator : AbstractValidator<AddUserDTO>
    {
        public AddUserModelValidator()
        {
            RuleFor(a => a.NameSurname).NotEmpty().WithMessage("Please enter user's name surname.");
            RuleFor(a => a.Mail).NotEmpty().WithMessage("Please enter user's mail address.");
            RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage("Please enter user's phone number.");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Please enter a password.");
            RuleFor(a => a.PasswordCheck).NotEmpty().WithMessage("Please enter password again.");
            RuleFor(a => a.Password).Equal(a => a.PasswordCheck).WithMessage("Passwords does not match.");
        }
    }
}

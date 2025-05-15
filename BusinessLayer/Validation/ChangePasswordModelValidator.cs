using DTOLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModelDTO>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(cpm => cpm.OldPassword).NotEmpty().NotNull().WithMessage("Please enter your current password.");
            RuleFor(cpm => cpm.NewPassword).NotEmpty().NotNull().WithMessage("Please enter new password.");
            RuleFor(cpm => cpm.CheckNewPassword).Equal(cpm => cpm.NewPassword).WithMessage("New password and confirm password doesn't match.");
        }
    }
}

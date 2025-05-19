using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Auth;

namespace BusinessLayer.Validation
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(cpm => cpm.OldPassword).NotEmpty().WithMessage("Please enter your current password");
            RuleFor(cpm => cpm.NewPassword).NotEmpty().WithMessage("Please enter new password");
            RuleFor(cpm => cpm.CheckNewPassword).Equal(cpm => cpm.NewPassword).WithMessage("New password and confirm password doesn't match");
        }
    }
}

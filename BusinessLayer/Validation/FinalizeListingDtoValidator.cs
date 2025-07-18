﻿using FluentValidation;
using MatchEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class FinalizeListingDtoValidator : AbstractValidator<FinalizeListingDto>
    {
        public FinalizeListingDtoValidator()
        {
            RuleFor(l => l.Earning).NotEmpty()
                .WithMessage("Please enter earning amount from this deal");

            RuleFor(l => l.Earning).Must(e => Convert.ToInt32(e) > 0)
                .WithMessage("Earning amount must be greater than 0");
            
        }
    }
}

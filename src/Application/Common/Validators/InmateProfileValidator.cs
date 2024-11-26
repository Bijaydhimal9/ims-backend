using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators
{
    public class InmateProfileValidator :AbstractValidator<InmateProfileRequestModel>
    {
        public InmateProfileValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.CitizenshipNumber).NotNull().NotEmpty().WithMessage("Citizenship number is required.");
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("Date of birth is required.");
        }
    }
}
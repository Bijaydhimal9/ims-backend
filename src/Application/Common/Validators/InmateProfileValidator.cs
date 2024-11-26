using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators;

/// <summary>
/// Inmate profile validator. <see cref="InmateProfileRequestModel"/>
/// </summary>
public class InmateProfileValidator : AbstractValidator<InmateProfileRequestModel>
{
    /// <summary>
    /// Inmate profile validator constructor. Initializes a new instance of the <see cref="InmateProfileValidator"/> class.
    /// </summary>
    public InmateProfileValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.CitizenshipNumber).NotNull().NotEmpty().WithMessage("Citizenship number is required.");
        RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("Date of birth is required.");
    }
}

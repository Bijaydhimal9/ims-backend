using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators;

/// <summary>
/// Login validator. <see cref="LoginRequestModel"/>
/// </summary>
public class LoginValidator : AbstractValidator<LoginRequestModel>
{
    /// <summary>
    /// Login validator constructor. Initializes a new instance of the <see cref="LoginValidator"/> class.
    /// </summary>
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required.");
    }
}
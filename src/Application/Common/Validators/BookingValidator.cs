using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators;

/// <summary>
/// Booking validator. <see cref="BookingRequestModel"/>
/// </summary>
public class BookingValidator : AbstractValidator<BookingRequestModel>
{
    /// <summary>
    /// Booking validator constructor. Initializes a new instance of the <see cref="BookingValidator"/> class.
    /// </summary>
    public BookingValidator()
    {
        RuleFor(x => x.FacilityName).NotNull().NotEmpty().WithMessage("Facility name is required");
        RuleFor(x => x.BookingLocation).NotNull().NotEmpty().WithMessage("Booking location is required.");
    }
}

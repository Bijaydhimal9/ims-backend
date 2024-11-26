using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators
{
    public class BookingValidator : AbstractValidator<BookingRequestModel>
    {
        public BookingValidator()
        {
            RuleFor(x => x.FacilityName).NotNull().NotEmpty().WithMessage("Facility name is required");
            RuleFor(x => x.BookingLocation).NotNull().NotEmpty().WithMessage("Booking location is required.");
        }
    }
}
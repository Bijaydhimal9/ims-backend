using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Application.Common.Validators;
public class BookingReleaseValidator : AbstractValidator<BookingReleaseRequestModel>
{

    public BookingReleaseValidator()
    {
        RuleFor(x => x.ReleaseReason).NotNull().NotEmpty().WithMessage("Release reason is required");
    }
}
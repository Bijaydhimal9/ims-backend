using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.RequestModels;
using FluentValidation;

namespace Web.Controllers
{
    public class BookingController : BaseApiController
    {
        private readonly IBookingService _bookingService;
        private readonly IValidator<BookingRequestModel> _validator;
        public BookingController(IBookingService bookingService,
        IValidator<BookingRequestModel> validator)
        {
            _bookingService = bookingService;
            _validator = validator;
        }

    }
}
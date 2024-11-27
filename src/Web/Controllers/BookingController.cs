using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Get paginated bookings
        /// </summary>
        /// <param name="pageNumber">the page number</param>
        /// <param name="pageSize">the page size</param>
        /// <param name="searchTerm">the search term</param>
        /// <returns>the paginated result of booking response model <see cref="BookingResponseModel"/></returns>
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<BookingListResponseModel>>> GetPaginatedBookingsAsync([FromQuery] BaseSearchCriteria criteria)
        {
            return await _bookingService.GetPaginatedBookingsAsync(criteria);
        }

        /// <summary>
        /// Create a booking
        /// </summary>
        /// <param name="model">the booking request model <see cref="BookingRequestModel"/></param>
        /// <returns>the created booking response model <see cref="BookingResponseModel"/></returns>
        [HttpPost]
        public async Task<ActionResult<BookingResponseModel>> Create([FromBody] BookingRequestModel model)
        {
            await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
            return await _bookingService.CreateBookingAsync(model, CurrentUser.UserId);
        }

        /// <summary>
        /// Get a booking by id or booking number
        /// </summary>
        /// <param name="id">the booking id or booking number</param>
        /// <returns>the booking response model <see cref="BookingResponseModel"/></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponseModel>> GetById(string id)
        {
            return await _bookingService.GetBookingByIdAsync(id, CurrentUser.UserId);
        }

        /// <summary>
        /// Update a booking
        /// </summary>
        /// <param name="id">the booking id or booking number</param>
        /// <param name="model">the booking request model <see cref="BookingRequestModel"/></param>
        /// <returns>the updated booking response model <see cref="BookingResponseModel"/></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<BookingResponseModel>> Update(string id, [FromBody] BookingRequestModel model)
        {
            await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
            return await _bookingService.UpdateBookingAsync(id, model, CurrentUser.UserId);
        }

        /// <summary>
        /// Update booking status
        /// </summary>
        /// <param name="id">the booking id or booking number</param>
        /// <param name="status">the booking status</param>
        /// <returns>no content</returns>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] BookingStatus status)
        {
            await _bookingService.UpdateBookingStatusAsync(id, status, CurrentUser.UserId);
            return NoContent();
        }

        /// <summary>
        /// Delete a booking
        /// </summary>
        /// <param name="id">the booking id or booking number</param>
        /// <returns>no content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _bookingService.DeleteBookingAsync(id, CurrentUser.UserId);
            return NoContent();
        }

    }
}
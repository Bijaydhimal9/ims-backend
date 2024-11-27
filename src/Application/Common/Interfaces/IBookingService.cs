using Application.Common.Dtos;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using Domain.Enums;

namespace Application.Common.Interfaces;
public interface IBookingService
{
    /// <summary>
    /// Get paginated bookings  
    /// </summary>
    /// <param name="pageNumber">the page number</param>
    /// <param name="pageSize">the page size</param>
    /// <param name="searchTerm">the search term</param>
    /// <returns>the paginated result of booking response model <see cref="BookingListResponseModel"/></returns>
    Task<PaginatedResult<BookingListResponseModel>> GetPaginatedBookingsAsync(BaseSearchCriteria criteria);

    /// <summary>
    /// Get booking by id
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    Task<BookingResponseModel> GetBookingByIdAsync(string bookingIdentity, string currentUserId);

    /// <summary>
    /// Create booking
    /// </summary>
    /// <param name="booking">the instance of booking request model <see cref="BookingRequestModel"/></param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    Task<BookingResponseModel> CreateBookingAsync(BookingRequestModel booking, string currentUserId);

    /// <summary>
    /// Update booking
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="booking">the instance of booking request model <see cref="BookingRequestModel"/></param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    Task<BookingResponseModel> UpdateBookingAsync(string bookingIdentity, BookingRequestModel booking, string currentUserId);

    /// <summary>
    /// Update booking status
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="status">the status</param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    Task<BookingResponseModel> UpdateBookingStatusAsync(string bookingIdentity, BookingStatus status, string currentUserId);

    /// <summary>
    /// Delete booking
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the task instance</returns>
    Task DeleteBookingAsync(string bookingIdentity, string currentUserId);
}
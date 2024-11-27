using System.Data;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using Domain.Enums;
using Infrastructure.Common;
using Infrastructure.Helpers;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Infrastructure.Services;
public class BookingService : BaseService, IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public BookingService(IUnitOfWork unitOfWork, ILogger<BookingService> logger) : base(unitOfWork, logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Get paginated bookings  
    /// </summary>
    /// <param name="criteria">the search criteria <see cref="BaseSearchCriteria"/></param>
    /// <returns>the paginated result of booking response model <see cref="BookingResponseModel"/></returns>

    public async Task<PaginatedResult<BookingListResponseModel>> GetPaginatedBookingsAsync(BaseSearchCriteria criteria)
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var parameters = new[]
            {
                new MySqlParameter("@PageNumber", criteria.Page),
                new MySqlParameter("@PageSize", criteria.Size),
                new MySqlParameter("@SearchTerm", criteria.Search ?? (object)DBNull.Value),
            };

            parameters.Append(new MySqlParameter
            {
                ParameterName = "@TotalCount",
                MySqlDbType = MySqlDbType.Int32,
                Direction = ParameterDirection.Output
            });

            var bookings = await _unitOfWork.DbContext.Database
                .SqlQueryRaw<BookingListResponseModel>(
                    "CALL GetPaginatedBookings(@PageNumber, @PageSize, @SearchTerm, @TotalCount)",
                    parameters)
                .ToListAsync();

            var totalCount = parameters.FirstOrDefault(p => p.ParameterName == "@TotalCount")?.Value;
            int totalCountInt = totalCount != null ? Convert.ToInt32(totalCount) : 0;

            return new PaginatedResult<BookingListResponseModel>
            {
                Items = bookings,
                TotalCount = totalCountInt,
                PageNumber = criteria.Page,
                PageSize = criteria.Size
            };
        });
    }

    /// <summary>
    /// Get booking by id
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>  
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    public async Task<BookingResponseModel> GetBookingByIdAsync(string bookingIdentity, string currentUserId)
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var parameters = new[]
            {
                 new MySqlParameter("@Id", bookingIdentity)
             };

            var entities = await _unitOfWork.DbContext.Bookings
                .FromSqlRaw("CALL GetBookingByIdentity(@Id)", parameters)
                .ToListAsync();

            var entity = entities.FirstOrDefault();
            if (entity == null)
            {
                _logger.LogError("Booking not found for identity: {BookingIdentity}", bookingIdentity);
                throw new EntityNotFoundException("Booking not found");
            }
            return entity.Adapt<BookingResponseModel>();
        });
    }

    /// <summary>
    /// Create booking
    /// </summary>
    /// <param name="booking">the instance of booking request model <see cref="BookingRequestModel"/></param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    public async Task<BookingResponseModel> CreateBookingAsync(BookingRequestModel booking, string currentUserId)
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var newBookingId = Guid.NewGuid().ToString();
            var bookingChargeId =Guid.NewGuid().ToString();
            var parameters = new[]
            {
                new MySqlParameter("@Id", newBookingId),
                new MySqlParameter("@InmateId", booking.InmateId),
                new MySqlParameter("@BookingNumber", CustomNewId.GenerateShortId()),
                new MySqlParameter("@BookingDate", booking.BookingDate),
                new MySqlParameter("@FacilityName", booking.FacilityName),
                new MySqlParameter("@BookingLocation", booking.BookingLocation),
                new MySqlParameter("@Status", BookingStatus.Active),
                new MySqlParameter("@CreatedOn", DateTime.UtcNow),
                new MySqlParameter("@CreatedBy", currentUserId),
                new MySqlParameter("@ChargeId", booking.ChargeId),
                new MySqlParameter("@BookingChargeId", bookingChargeId),
            };

            await _unitOfWork.DbContext.Database
                .ExecuteSqlRawAsync("CALL CreateBooking(@Id, @InmateId, @BookingNumber, @BookingDate, @FacilityName, " +
                    "@BookingLocation, @Status, @CreatedOn, @CreatedBy,@ChargeId,@BookingChargeId)", parameters);

            return await GetBookingByIdAsync(newBookingId, currentUserId);
        });
    }

    /// <summary>
    /// Update booking
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="booking">the instance of booking request model <see cref="BookingRequestModel"/></param>
    /// <param name="currentUserId">the current user id</param>
    /// <returns>the instance of booking response model <see cref="BookingResponseModel"/></returns>
    public async Task<BookingResponseModel> UpdateBookingAsync(string bookingIdentity, BookingRequestModel booking, string currentUserId)
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var existingBooking = await GetBookingByIdAsync(bookingIdentity, currentUserId);
            var parameters = new[]
            {
                new MySqlParameter("@Id", bookingIdentity),
                new MySqlParameter("@InmateId", booking.InmateId),
                new MySqlParameter("@BookingDate", booking.BookingDate),
                new MySqlParameter("@FacilityName", booking.FacilityName),
                new MySqlParameter("@BookingLocation", booking.BookingLocation),
                new MySqlParameter("@UpdatedOn", DateTime.UtcNow),
                new MySqlParameter("@UpdatedBy", currentUserId),
                new MySqlParameter("@ChargeId", booking.ChargeId),
            };

            await _unitOfWork.DbContext.Database
                .ExecuteSqlRawAsync("CALL UpdateBooking(@Id, @InmateId, @BookingDate, @FacilityName, " +
                    "@BookingLocation, @UpdatedOn, @UpdatedBy,@ChargeId)", parameters);

            return await GetBookingByIdAsync(bookingIdentity, currentUserId);
        });
    }

    /// <summary>   
    /// Update booking status
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="status">the status</param>
    /// <param name="currentUserId">the current user id</param>
    public async Task<BookingResponseModel> UpdateBookingStatusAsync(string bookingIdentity, BookingStatus status, string currentUserId)
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var existingBooking = await GetBookingByIdAsync(bookingIdentity, currentUserId);

            var parameters = new[]
            {
                new MySqlParameter("@Id", existingBooking.Id),
                new MySqlParameter("@Status", status.ToString())
            };

            await _unitOfWork.DbContext.Database
                .ExecuteSqlRawAsync("CALL BookingStatusUpdate(@Id, @Status)", parameters);

            return await GetBookingByIdAsync(bookingIdentity, currentUserId);
        });
    }

    /// <summary>
    /// Delete booking
    /// </summary>
    /// <param name="bookingIdentity">the booking id or booking number</param>
    /// <param name="currentUserId">the current user id</param>
    public async Task DeleteBookingAsync(string bookingIdentity, string currentUserId)
    {
        await ExecuteAsync(async () =>
        {
            var existingBooking = await GetBookingByIdAsync(bookingIdentity, currentUserId);
            var parameters = new[]
            {
                new MySqlParameter("@BookingId", existingBooking.Id)
            };

            await _unitOfWork.DbContext.Database
                .ExecuteSqlRawAsync("CALL DeleteBooking(@BookingId)", parameters);
        });
    }

}
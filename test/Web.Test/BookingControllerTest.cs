using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Web.Controllers;


namespace Web.Test;
public class BookingControllerTest
{
    private readonly Mock<IBookingService> _mockBookingService;
    private readonly Mock<IValidator<BookingRequestModel>> _mockValidator;
    private readonly BookingController _controller;
    private readonly string _userId = Guid.NewGuid().ToString();

    /// <summary>
    /// Booking controller test constructor. Initializes a new instance of the <see cref="BookingControllerTest"/> class.
    /// </summary>
    public BookingControllerTest()
    {
        _mockBookingService = new Mock<IBookingService>();
        _mockValidator = new Mock<IValidator<BookingRequestModel>>();
        _controller = new BookingController(_mockBookingService.Object, _mockValidator.Object);

        // Setup controller context with user claims
        var claims = new List<Claim>
        {
            new Claim("uid", _userId),
            new Claim(ClaimTypes.Email, "test@example.com")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var controllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        _controller.ControllerContext = controllerContext;
    }

    /// <summary>
    /// Test for getting paginated bookings.
    /// </summary>
    [Fact]
    public async Task GetPaginatedBookings_ReturnsOkResult_WithPaginatedListOfBookings()
    {
        // Arrange
        var criteria = new BaseSearchCriteria { Page = 1, Size = 10 };
        var paginatedBookings = new PaginatedResult<BookingListResponseModel>
        {
            Items = new List<BookingListResponseModel>
            {
                new() { Id = Guid.NewGuid(), BookingNumber = "B001" },
                new() { Id = Guid.NewGuid(), BookingNumber = "B002" }
            },
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 2
        };

        _mockBookingService.Setup(service => service.GetPaginatedBookingsAsync(criteria))
            .ReturnsAsync(paginatedBookings);

        // Act
        var result = await _controller.GetPaginatedBookingsAsync(criteria);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedResult<BookingListResponseModel>>>(result);
        var returnedBookings = Assert.IsType<PaginatedResult<BookingListResponseModel>>(actionResult.Value);
        Assert.Equal(2, returnedBookings.Items.Count());
        Assert.Equal(1, returnedBookings.PageNumber);
        Assert.Equal(2, returnedBookings.TotalCount);
    }

    /// <summary>
    /// Test for creating a new booking.
    /// </summary>
    [Fact]
    public async Task Create_ReturnsCreatedBooking_WhenModelIsValid()
    {
        // Arrange
        var bookingRequest = new BookingRequestModel
        {
            InmateId = Guid.NewGuid(),
            BookingDate = DateTime.UtcNow,
            FacilityName = "Test Facility",
            BookingLocation = "Test Location"
        };

        var createdBooking = new BookingResponseModel
        {
            Id = Guid.NewGuid(),
            BookingNumber = "B001"
        };

        _mockValidator.Setup(v => v.ValidateAsync(bookingRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockBookingService.Setup(service => service.CreateBookingAsync(bookingRequest, It.IsAny<string>()))
            .ReturnsAsync(createdBooking);

        // Act
        var result = await _controller.Create(bookingRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BookingResponseModel>>(result);
        var returnedBooking = Assert.IsType<BookingResponseModel>(actionResult.Value);
        Assert.Equal(createdBooking.Id, returnedBooking.Id);
        Assert.Equal(createdBooking.BookingNumber, returnedBooking.BookingNumber);
    }

    /// <summary>
    /// Test for getting a booking by id.
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsBooking_WhenBookingExists()
    {
        // Arrange
        var bookingId = "B001";
        var booking = new BookingResponseModel
        {
            Id = Guid.NewGuid(),
            BookingNumber = bookingId
        };

        _mockBookingService.Setup(service => service.GetBookingByIdAsync(bookingId, It.IsAny<string>()))
            .ReturnsAsync(booking);

        // Act
        var result = await _controller.GetById(bookingId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BookingResponseModel>>(result);
        var returnedBooking = Assert.IsType<BookingResponseModel>(actionResult.Value);
        Assert.Equal(booking.BookingNumber, returnedBooking.BookingNumber);
    }

    /// <summary>
    /// Test for updating a booking.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsUpdatedBooking_WhenModelIsValid()
    {
        // Arrange
        var bookingId = "B001";
        var bookingRequest = new BookingRequestModel
        {
            InmateId = Guid.NewGuid(),
            BookingDate = DateTime.UtcNow,
            FacilityName = "Updated Facility",
            BookingLocation = "Updated Location"
        };

        var updatedBooking = new BookingResponseModel
        {
            Id = Guid.NewGuid(),
            BookingNumber = bookingId,
            FacilityName = "Updated Facility"
        };

        _mockValidator.Setup(v => v.ValidateAsync(bookingRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockBookingService.Setup(service => service.UpdateBookingAsync(bookingId, bookingRequest, It.IsAny<string>()))
            .ReturnsAsync(updatedBooking);

        // Act
        var result = await _controller.Update(bookingId, bookingRequest);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BookingResponseModel>>(result);
        var returnedBooking = Assert.IsType<BookingResponseModel>(actionResult.Value);
        Assert.Equal(updatedBooking.FacilityName, returnedBooking.FacilityName);
    }

    /// <summary>
    /// Test for updating a booking status.
    /// </summary>
    [Fact]
    public async Task UpdateStatus_ReturnsNoContent_WhenUpdateSuccessful()
    {
        // Arrange
        var bookingId = "B001";
        var status = BookingStatus.Released;
        var updatedBooking = new BookingResponseModel
        {
            Id = Guid.NewGuid(),
            BookingNumber = bookingId
        };

        _mockBookingService.Setup(service => service.UpdateBookingStatusAsync(bookingId, status, It.IsAny<string>()))
            .ReturnsAsync(updatedBooking);

        // Act
        var result = await _controller.UpdateStatus(bookingId, status);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    /// <summary>
    /// Test for deleting a booking.
    /// </summary>
    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleteSuccessful()
    {
        // Arrange
        var bookingId = "B001";

        _mockBookingService.Setup(service => service.DeleteBookingAsync(bookingId, It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(bookingId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
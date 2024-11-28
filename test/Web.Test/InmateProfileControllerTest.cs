using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Web.Controllers;

namespace Web.Test;

/// <summary>
/// Inmate profile controller test class.<see cref="InmateProfileController"/>
/// </summary>
public class InmateProfileControllerTest
{
    private readonly Mock<IInmateProfileService> _mockInmateProfileService;
    private readonly Mock<IValidator<InmateProfileRequestModel>> _mockValidator;
    private readonly InmateProfileController _controller;
    private readonly string _userId = Guid.NewGuid().ToString();

    /// <summary>
    /// Inmate profile controller test constructor. Initializes a new instance of the <see cref="InmateProfileControllerTest"/> class.
    /// </summary>
    public InmateProfileControllerTest()
    {
        _mockInmateProfileService = new Mock<IInmateProfileService>();
        _mockValidator = new Mock<IValidator<InmateProfileRequestModel>>();
        _controller = new InmateProfileController(_mockInmateProfileService.Object, _mockValidator.Object);

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
    /// Test for getting paginated inmates.
    /// </summary>
    [Fact]
    public async Task GetPaginatedInmates_ReturnsOkResult_WithPaginatedListOfInmates()
    {
        // Arrange
        var criteria = new BaseSearchCriteria { Page = 1, Size = 10 };
        var paginatedInmates = new PaginatedResult<InmateProfileResponseModel>
        {
            Items = new List<InmateProfileResponseModel>
            {
                new() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.UtcNow.AddYears(-30), CitizenshipNumber = "1234567890" },
                new() { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", DateOfBirth = DateTime.UtcNow.AddYears(-25), CitizenshipNumber = "0987654321" }
            },
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 2
        };

        _mockInmateProfileService.Setup(service => service.GetPaginatedInmatesAsync(criteria))
            .ReturnsAsync(paginatedInmates);

        // Act
        var result = await _controller.GetPaginatedInmates(criteria);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedInmates = Assert.IsType<PaginatedResult<InmateProfileResponseModel>>(okResult.Value);
        Assert.Equal(2, returnedInmates.Items.Count());
        Assert.Equal(1, returnedInmates.PageNumber);
        Assert.Equal(2, returnedInmates.TotalCount);
    }

    /// <summary>
    /// Test for creating a new inmate profile.
    /// </summary>
    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenModelIsValid()
    {
        // Arrange
        var inmateRequest = new InmateProfileRequestModel
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-30)
        };

        var createdInmate = new InmateProfileResponseModel
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            CitizenshipNumber = "1234567890"
        };

        _mockValidator.Setup(v => v.ValidateAsync(inmateRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockInmateProfileService.Setup(service => service.CreateAsync(inmateRequest, It.IsAny<string>()))
            .ReturnsAsync(createdInmate);

        // Act
        var result = await _controller.Create(inmateRequest);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(InmateProfileController.GetById), createdAtActionResult.ActionName);
        var returnedInmate = Assert.IsType<InmateProfileResponseModel>(createdAtActionResult.Value);
        Assert.Equal(createdInmate.Id, returnedInmate.Id);
        Assert.Equal(createdInmate.FirstName, returnedInmate.FirstName);
    }

    /// <summary>
    /// Test for getting an inmate by id.
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsOkResult_WhenInmateExists()
    {
        // Arrange
        var inmateId = Guid.NewGuid();
        var inmate = new InmateProfileResponseModel
        {
            Id = inmateId,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            CitizenshipNumber = "1234567890"
        };

        _mockInmateProfileService.Setup(service => service.GetByIdAsync(inmateId, It.IsAny<string>()))
            .ReturnsAsync(inmate);

        // Act
        var result = await _controller.GetById(inmateId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedInmate = Assert.IsType<InmateProfileResponseModel>(okResult.Value);
        Assert.Equal(inmate.Id, returnedInmate.Id);
    }

    /// <summary>
    /// Test for getting an inmate by id when the inmate does not exist.
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenInmateDoesNotExist()
    {
        // Arrange
        var inmateId = Guid.NewGuid();
        _mockInmateProfileService.Setup(service => service.GetByIdAsync(inmateId, It.IsAny<string>()))
            .ReturnsAsync((InmateProfileResponseModel)null);

        // Act
        var result = await _controller.GetById(inmateId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>ß
    /// Test for updating an inmate profile.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsOkResult_WhenModelIsValid()
    {
        // Arrange
        var inmateId = Guid.NewGuid();
        var inmateRequest = new InmateProfileRequestModel
        {
            FirstName = "John",
            LastName = "Updated",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            CitizenshipNumber = "1234567890"
        };

        var updatedInmate = new InmateProfileResponseModel
        {
            Id = inmateId,
            FirstName = "John",
            LastName = "Updated",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            CitizenshipNumber = "1234567890"
        };

        _mockValidator.Setup(v => v.ValidateAsync(inmateRequest, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockInmateProfileService.Setup(service => service.UpdateAsync(inmateId, inmateRequest, It.IsAny<string>()))
            .ReturnsAsync(updatedInmate);

        // Act
        var result = await _controller.Update(inmateId, inmateRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedInmate = Assert.IsType<InmateProfileResponseModel>(okResult.Value);
        Assert.Equal(updatedInmate.LastName, returnedInmate.LastName);
    }

    /// <summary>
    /// Test for deleting an inmate profile.
    /// </summary>
    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleteSuccessful()
    {
        // Arrange
        var inmateId = Guid.NewGuid();

        _mockInmateProfileService.Setup(service => service.DeleteAsync(inmateId, It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(inmateId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
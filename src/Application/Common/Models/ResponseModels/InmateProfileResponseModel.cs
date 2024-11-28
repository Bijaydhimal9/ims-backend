using Domain.Enums;

namespace Application.Common.Models.ResponseModels;

/// <summary>
/// Represents an inmate profile response model
/// </summary>
public class InmateProfileResponseModel
{
    /// <summary>
    /// Gets or sets the id of the inmate profile
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the inmate
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the middle name of the inmate
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the inmate
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the full name of the inmate
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the gender of the inmate
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the inmate
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the citizenship number of the inmate
    /// </summary>
    public required string CitizenshipNumber { get; set; }

    /// <summary>
    /// Gets or sets the address of the inmate
    /// </summary>
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the phone number of the inmate
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the emergency contact of the inmate
    /// </summary>
    public string EmergencyContact { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the emergency contact phone of the inmate
    /// </summary>
    public string EmergencyContactPhone { get; set; } = string.Empty;
}

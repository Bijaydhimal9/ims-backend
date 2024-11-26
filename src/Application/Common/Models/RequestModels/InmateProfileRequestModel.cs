using Domain.Enums;

namespace Application.Common.Models.RequestModels;

/// <summary>
/// Represents a request model for an inmate profile
/// </summary>
public class InmateProfileRequestModel
{
    /// <summary>
    /// Gets or sets the first name of the inmate
    /// </summary>
    public string FirstName { get; set; } = null!;
    /// <summary>
    /// Gets or sets the middle name of the inmate
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Gets or sets the last name of the inmate
    /// </summary>
    public string LastName { get; set; } = null!;
    /// <summary>
    /// Gets or sets the date of birth of the inmate
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    /// <summary>
    /// Gets or sets the citizenship number of the inmate
    /// </summary>
    public string CitizenshipNumber { get; set; } = null!;
    /// <summary>
    /// Gets or sets the gender of the inmate
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// Gets or sets the address of the inmate
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// Gets or sets the phone number of the inmate
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Gets or sets the emergency contact of the inmate
    /// </summary>
    public string? EmergencyContact { get; set; }
    /// <summary>
    /// Gets or sets the emergency contact phone number of the inmate
    /// </summary>
    public string? EmergencyContactPhone { get; set; }
}

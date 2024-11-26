using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents an inmate profile
/// </summary>
public class InmateProfile : AuditableEntity
{
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
    /// Gets or sets the date of birth of the inmate
    /// </summary>
    public required DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the citizenship number of the inmate
    /// </summary>
    public required string CitizenshipNumber { get; set; }

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

    /// <summary>
    /// Gets or sets the application user associated with the inmate
    /// </summary>
    public virtual ApplicationUser? ApplicationUser { get; set; }

    /// <summary>
    /// Gets or sets the bookings associated with the inmate
    /// </summary>
    public virtual IList<Booking> Bookings { get; set; } = new List<Booking>();
}

using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Represents a booking
/// </summary>
public class Booking : AuditableEntity
{
    /// <summary>       
    /// Get or set inmate id
    /// </summary>
    public Guid InmateId { get; set; }

    /// <summary>
    /// Get or set booking number
    /// </summary>
    public required string BookingNumber { get; set; }

    /// <summary>
    /// Get or set booking date
    /// </summary>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Get or set facility name
    /// </summary>
    public required string FacilityName { get; set; }

    /// <summary>
    /// Get or set booking location
    /// </summary>
    public required string BookingLocation { get; set; }

    /// <summary>
    /// Get or set status
    /// </summary>
    public BookingStatus Status { get; set; }

    /// <summary>
    /// Get or set release date
    /// </summary>
    public DateTime? ReleaseDate { get; set; }

    /// <summary>
    /// Get or set release reason
    /// </summary>
    public string? ReleaseReason { get; set; }

    /// <summary>
    /// Get or set comments
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Get or set inmate profile
    /// </summary>
    public virtual InmateProfile? InmateProfile { get; set; }

    /// <summary>
    /// Get or set application user
    /// </summary>
    public virtual ApplicationUser? ApplicationUser { get; set; }

    /// <summary>
    /// Get or set charges
    /// </summary>
    public virtual IList<Charge> Charges { get; set; } = new List<Charge>();
}

namespace Domain.Enums;

/// <summary>
/// Represents the status of a booking
/// </summary>
public enum BookingStatus
{
    /// <summary>
    /// Represents an active booking
    /// </summary>
    Active = 1,

    /// <summary>
    /// Represents a released booking
    /// </summary>
    Released = 2,

    /// <summary>
    /// Represents a transferred booking
    /// </summary>
    Transferred = 3,

    /// <summary>
    /// Represents an escaped booking
    /// </summary>
    Escaped = 4,

    /// <summary>
    /// Represents a deceased booking
    /// </summary>
    Deceased = 5
}

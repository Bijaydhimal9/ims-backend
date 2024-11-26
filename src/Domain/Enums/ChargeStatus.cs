namespace Domain.Enums;

/// <summary>
/// Represents the status of a charge
/// </summary>
public enum ChargeStatus
{

    /// <summary>
    /// Represents an active charge
    /// </summary>
    Active = 1,
    /// <summary>
    /// Represents a released charge
    /// </summary>
    Released = 2,
    /// <summary>
    /// Represents a transferred charge 
    /// </summary>
    Transferred = 3,

    /// <summary>
    /// Represents an escaped charge
    /// </summary>
    Escaped = 4,

    /// <summary>
    /// Represents a deceased charge
    /// </summary>
    Deceased = 5
}

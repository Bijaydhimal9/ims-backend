namespace Application.Common.Models.RequestModels;

/// <summary>
/// Represents a booking request model
/// </summary>
public class BookingRequestModel
{
    /// <summary>
    /// Gets or sets the inmate id
    /// </summary>
    public Guid InmateId { get; set; }

    /// <summary>
    /// Gets or sets the booking date
    /// </summary>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Gets or sets the facility name
    /// </summary>
    public string FacilityName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the booking location
    /// </summary>
    public string BookingLocation { get; set; } = null!;

    /// <summary>
    /// Gets or sets the charges
    /// </summary>
    public Guid ChargeId { get; set; } = new();
}

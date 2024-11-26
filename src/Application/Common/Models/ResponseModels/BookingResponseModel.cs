namespace Application.Common.Models.ResponseModels;

/// <summary>
/// Represents a booking response model
/// </summary>
public class BookingResponseModel
{
    /// <summary>
    /// Gets or sets the id of the booking
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the booking number
    /// </summary>
    public string? BookingNumber { get; set; }
    /// <summary>
    /// Gets or sets the inmate id
    /// </summary>
    public Guid InmateId { get; set; }
    /// <summary>
    /// Gets or sets the inmate name
    /// </summary>
    public string? BookingLocation { get; set; }
    /// <summary>
    /// Gets or sets the facility name
    /// </summary>
    public string? FacilityName { get; set; }
    /// <summary>
    /// Gets or sets the booking date
    /// </summary>
    public DateTime BookingDate { get; set; }
    /// <summary>
    /// Gets or sets the expected release date
    /// </summary>
    public DateTime? ExpectedReleaseDate { get; set; }
    /// <summary>
    /// Gets or sets the actual release date
    /// </summary>
    public DateTime? ActualReleaseDate { get; set; }
    /// <summary>
    /// Gets or sets the release reason
    /// </summary>
    public string? ReleaseReason { get; set; }
    /// <summary>
    /// Gets or sets the charges
    /// </summary>
    public List<BookingChargeResponse> Charges { get; set; } = new();
    /// <summary>
    /// Gets or sets the created at date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the updated at date
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

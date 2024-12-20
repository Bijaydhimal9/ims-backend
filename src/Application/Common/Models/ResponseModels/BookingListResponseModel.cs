using Domain.Enums;

namespace Application.Common.Models.ResponseModels;

public class BookingListResponseModel
{
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
    /// Get or set status
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the inmate name
    /// </summary>
    public string InmateName { get; set; }
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

}
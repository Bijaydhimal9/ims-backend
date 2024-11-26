using Domain.Enums;

namespace Application.Common.Models.ResponseModels;

/// <summary>
/// Represents a booking charge response model
/// </summary>
public class BookingChargeResponse
{
    /// <summary>
    /// Gets or sets the id of the booking charge
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the charge
    /// </summary>
    public string? ChargeName { get; set; }
    /// <summary>
    /// Gets or sets the code of the charge
    /// </summary>
    public string? ChargeCode { get; set; }
    /// <summary>
    /// Gets or sets the description of the charge
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Gets or sets the status of the charge
    /// </summary>
    public ChargeStatus Status { get; set; }
    /// <summary>
    /// Gets or sets the date of the charge
    /// </summary>
    public DateTime ChargeDate { get; set; }
}

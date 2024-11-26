namespace Application.Common.Models.RequestModels;

/// <summary>
/// Represents a booking charge request model
/// </summary>
public class BookingChargeModel
{
    /// <summary>
    /// Gets or sets the charge code
    /// </summary>
    public string ChargeCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the charge
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the severity of the charge
    /// </summary>
    public string? Severity { get; set; }

    /// <summary>
    /// Gets or sets the charge date
    /// </summary>
    public DateTime ChargeDate { get; set; }
}

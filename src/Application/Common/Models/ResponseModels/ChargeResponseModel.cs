using Domain.Enums;

namespace Application.Common.Models.ResponseModels;
public class ChargeResponseModel
{
    /// <summary>
    /// Gets or sets the charge id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the charge name
    /// </summary>
    public required string ChargeName { get; set; }

    /// <summary>
    /// Gets or sets the charge code
    /// </summary>
    public required string ChargeCode { get; set; }

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the status
    /// </summary>
    public ChargeStatus Status { get; set; }
}
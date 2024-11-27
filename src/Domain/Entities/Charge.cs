using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Charge : AuditableEntity
{
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
   
    /// <summary>
    /// Gets or sets the application user
    /// </summary>
    public virtual ApplicationUser? ApplicationUser { get; set; }
}

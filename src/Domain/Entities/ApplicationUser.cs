using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the bookings
    /// </summary>
    public List<Booking> Bookings { get; set; } = new List<Booking>();

    /// <summary>
    /// Gets or sets the inmates
    /// </summary>
    public List<InmateProfile> Inmates { get; set; } = new List<InmateProfile>();

    /// <summary>
    /// Gets or sets the charges
    /// </summary>
    public IList<Charge> Charges { get; set; } = new List<Charge>();
     public IList<BookingCharge> BookingCharges { get; set; } = new List<BookingCharge>();
}

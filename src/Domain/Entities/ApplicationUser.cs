using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<InmateProfile> Inmates { get; set; } = new List<InmateProfile>();
        public IList<Charge> Charges { get; set; } = new List<Charge>();
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
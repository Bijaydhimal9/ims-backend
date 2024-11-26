using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<Inmate> Inmates { get; set; } = new List<Inmate>();
        public List<Charge> Charges { get; set; } = new List<Charge>();
    }
}
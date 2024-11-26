using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public Guid InmateId { get; set; }
        public string BookingNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public string FacilityName { get; set; } 
        public string BookingLocation { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public BookingStatus Status { get; set; }
        public string ReleaseReason { get; set; }
        public string Comments { get; set; } 
        public virtual InmateProfile InmateProfile { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public virtual IList<Charge> Charges { get; set; } = new List<Charge>();
    }
}
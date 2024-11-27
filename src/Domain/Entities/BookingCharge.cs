using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class BookingCharge : AuditableEntity
    {
        public Guid ChargeId { get; set; }
        public virtual Charge Charge { get; set; }
        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Charge : AuditableEntity
    {
        public Guid BookingId { get; set; }
        public string ChargeName { get; set; }
        public string ChargeCode { get; set; }
        public string Description { get; set; }
        public ChargeStatus Status { get; set; }
        public DateTime? ChargeDate { get; set; }
        public virtual Booking Booking { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
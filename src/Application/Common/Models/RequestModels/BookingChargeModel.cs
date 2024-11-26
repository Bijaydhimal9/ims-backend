using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Models.RequestModels
{
    public class BookingChargeModel
    {
        public string ChargeCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public DateTime ChargeDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Common.Models.ResponseModels
{
    public class BookingChargeResponse
    {
        public Guid Id { get; set; }
        public string ChargeName { get; set; } = string.Empty;
        public string ChargeCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ChargeStatus Status { get; set; }
        public DateTime ChargeDate { get; set; }
    }
}
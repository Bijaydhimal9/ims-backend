using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Models.ResponseModels
{
    public class BookingResponseModel
    {
        public Guid Id { get; set; }
        public string BookingNumber { get; set; } = string.Empty;
        public Guid InmateProfileId { get; set; }
        public string InmateName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public DateTime? ExpectedReleaseDate { get; set; }
        public DateTime? ActualReleaseDate { get; set; }
        public string ReleaseReason { get; set; } = string.Empty;
        public List<BookingChargeResponse> Charges { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
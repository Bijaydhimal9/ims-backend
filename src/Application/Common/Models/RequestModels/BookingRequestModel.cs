using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Common.Models.RequestModels
{
    public class BookingRequestModel
    {
        public Guid InmateProfileId { get; set; }
        public DateTime BookingDate { get; set; }
        public string FacilityName { get; set; } 
        public string BookingLocation { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseReason { get; set; }
        public string Comments { get; set; }
         public List<BookingChargeModel> Charges { get; set; } = new();
    }
}
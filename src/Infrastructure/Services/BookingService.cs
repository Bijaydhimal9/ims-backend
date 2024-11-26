using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class BookingService : BaseService, IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public BookingService(IUnitOfWork unitOfWork, ILogger<BookingService> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}
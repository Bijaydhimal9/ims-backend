using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using AutoMapper;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class InmateProfileService : BaseService, IInmateProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

         private readonly IMapper _mapper;
        public InmateProfileService(IUnitOfWork unitOfWork,
         ILogger<InmateProfileService> logger,
         IMapper mapper) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<InmateProfileResponseModel> CreateAsync(InmateProfileRequestModel model, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<InmateProfileResponseModel> GetByIdAsync(Guid id, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<InmateProfileResponseModel> UpdateAsync(Guid id, InmateProfileRequestModel model, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
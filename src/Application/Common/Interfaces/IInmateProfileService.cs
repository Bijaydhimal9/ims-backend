using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;

namespace Application.Common.Interfaces
{
    public interface IInmateProfileService
    {
        Task<InmateProfileResponseModel> CreateAsync(InmateProfileRequestModel model, string currentUserId);
        Task<InmateProfileResponseModel> UpdateAsync(Guid id, InmateProfileRequestModel model, string currentUserId);
        Task DeleteAsync(Guid id, string currentUserId);
        Task<InmateProfileResponseModel> GetByIdAsync(Guid id, string currentUserId);
    }
}
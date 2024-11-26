using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;

namespace Application.Common.Interfaces
{
    public interface IInmateProfileService
    {
        /// <summary>
        /// Get paginated inmates
        /// </summary>
        /// <param name="criteria">The search criteria <see cref="BaseSearchCriteria"/></param>
        /// <returns>The paginated result of inmate profile response model <see cref="InmateProfileResponseModel"/></returns>
        Task<PaginatedResult<InmateProfileResponseModel>> GetPaginatedInmatesAsync(BaseSearchCriteria criteria);

        /// <summary>
        /// Create async
        /// </summary>
        /// <param name="model">Inmate profile request model</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>Inmate profile response model</returns>
        Task<InmateProfileResponseModel> CreateAsync(InmateProfileRequestModel model, string currentUserId);

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Inmate profile request model</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>Inmate profile response model</returns>
        Task<InmateProfileResponseModel> UpdateAsync(Guid id, InmateProfileRequestModel model, string currentUserId);

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="currentUserId">Current user id</param>
        Task DeleteAsync(Guid id, string currentUserId);

        /// <summary>
        /// Get by id async
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>Inmate profile response model</returns>
        Task<InmateProfileResponseModel> GetByIdAsync(Guid id, string currentUserId);
    }
}
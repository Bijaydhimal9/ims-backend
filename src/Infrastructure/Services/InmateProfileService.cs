using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using Domain.Entities;
using Infrastructure.Common;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class InmateProfileService : BaseService, IInmateProfileService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        /// <param name="logger">The logger</param>
        public InmateProfileService(IUnitOfWork unitOfWork,
         ILogger<InmateProfileService> logger
         ) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Get paginated inmates
        /// </summary>
        /// <param name="criteria">The search criteria <see cref="BaseSearchCriteria"/></param>
        /// <returns>The paginated result of inmate profile response model <see cref="InmateProfileResponseModel"/></returns>
        public async Task<PaginatedResult<InmateProfileResponseModel>> GetPaginatedInmatesAsync(BaseSearchCriteria criteria)
        {
            return await ExecuteWithResultAsync(async () =>
            {
                var query = _unitOfWork.DbContext.InmateProfiles.AsQueryable();
                if (!string.IsNullOrEmpty(criteria.Search))
                {
                    query = query.Where(x => x.FirstName.Contains(criteria.Search) || x.LastName.Contains(criteria.Search));
                }
                query = query.OrderByDescending(x => x.CreatedOn);
                var paginatedResult = await query.ToPagedListAsync(criteria.Page, criteria.Size);
                return paginatedResult.Adapt<PaginatedResult<InmateProfileResponseModel>>();
            });
        }

        /// <summary>
        /// Create async
        /// </summary>
        /// <param name="model">Inmate profile request model</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>Inmate profile response model</returns>
        public async Task<InmateProfileResponseModel> CreateAsync(InmateProfileRequestModel model, string currentUserId)
        {
            return await ExecuteWithResultAsync(async () =>
            {
                var inmateProfile = model.Adapt<InmateProfile>();
                inmateProfile.CreatedBy = currentUserId;
                inmateProfile.CreatedOn = DateTime.UtcNow;
                await _unitOfWork.DbContext.InmateProfiles.AddAsync(inmateProfile);
                await _unitOfWork.DbContext.SaveChangesAsync();
                return inmateProfile.Adapt<InmateProfileResponseModel>();
            });
        }

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="id">Inmate profile id</param>
        /// <param name="currentUserId">Current user id</param>
        public async Task DeleteAsync(Guid id, string currentUserId)
        {
            await ExecuteAsync(async () =>
            {
                var inmateProfile = await _unitOfWork.DbContext.InmateProfiles.FindAsync(id);
                if (inmateProfile == null)
                {
                    _logger.LogError($"Inmate profile with id {id} not found");
                    throw new EntityNotFoundException("Inmate profile not found");
                }
                _unitOfWork.DbContext.InmateProfiles.Remove(inmateProfile);
                await _unitOfWork.DbContext.SaveChangesAsync();
            });
        }

        /// <summary>
        /// Get by id async
        /// </summary>
        /// <param name="id">Inmate profile id</param>
        /// <returns>Inmate profile response model</returns>
        public async Task<InmateProfileResponseModel> GetByIdAsync(Guid id, string currentUserId)
        {
            return await ExecuteWithResultAsync(async () =>
            {
                var inmateProfile = await _unitOfWork.DbContext.InmateProfiles.FindAsync(id);
                return inmateProfile.Adapt<InmateProfileResponseModel>();
            });
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="id">Inmate profile id</param>
        /// <param name="model">Inmate profile request model</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>Inmate profile response model</returns>
        public async Task<InmateProfileResponseModel> UpdateAsync(Guid id, InmateProfileRequestModel model, string currentUserId)
        {
            return await ExecuteWithResultAsync(async () =>
            {
                var existingInmateProfile = await _unitOfWork.DbContext.InmateProfiles.FindAsync(id);
                if (existingInmateProfile == null)
                {
                    _logger.LogError($"Inmate profile with id {id} not found");
                    throw new EntityNotFoundException("Inmate profile not found");
                }
                existingInmateProfile.FirstName = model.FirstName;
                existingInmateProfile.MiddleName = model.MiddleName;
                existingInmateProfile.LastName = model.LastName;
                existingInmateProfile.DateOfBirth = model.DateOfBirth;
                existingInmateProfile.CitizenshipNumber = model.CitizenshipNumber;
                existingInmateProfile.Gender = model.Gender;
                existingInmateProfile.Address = model.Address;
                existingInmateProfile.PhoneNumber = model.PhoneNumber;
                existingInmateProfile.EmergencyContact = model.EmergencyContact;
                existingInmateProfile.EmergencyContactPhone = model.EmergencyContactPhone;
                existingInmateProfile.UpdatedBy = currentUserId;
                existingInmateProfile.UpdatedOn = DateTime.UtcNow;
                await _unitOfWork.DbContext.SaveChangesAsync();
                return existingInmateProfile.Adapt<InmateProfileResponseModel>();
            });
        }
    }
}
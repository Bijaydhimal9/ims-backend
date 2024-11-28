using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
public class InmateProfileController : BaseApiController
{
    private readonly IInmateProfileService _inmateProfileService;
    private readonly IValidator<InmateProfileRequestModel> _validator;

    /// <summary>
    /// Inmate profile controller constructor
    /// </summary>
    /// <param name="inmateProfileService">The inmate profile service <see cref="IInmateProfileService"/></param>
    /// <param name="validator">The validator <see cref="IValidator{InmateProfileRequestModel}"/></param>
    public InmateProfileController(
        IInmateProfileService inmateProfileService,
    IValidator<InmateProfileRequestModel> validator)
    {
        _inmateProfileService = inmateProfileService;
        _validator = validator;
    }

    /// <summary>
    /// Get paginated inmates
    /// </summary>
    /// <param name="criteria">The search criteria <see cref="BaseSearchCriteria"/></param>
    /// <returns>The paginated result of inmate profile response model <see cref="InmateProfileResponseModel"/></returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<InmateProfileResponseModel>>> GetPaginatedInmates([FromQuery] BaseSearchCriteria criteria)
    {
        var result = await _inmateProfileService.GetPaginatedInmatesAsync(criteria);
        return Ok(result);
    }

    /// <summary>
    /// Create a new inmate profile
    /// </summary>
    /// <param name="model">The inmate profile request model <see cref="InmateProfileRequestModel"/></param>
    /// <returns>The inmate profile response model <see cref="InmateProfileResponseModel"/></returns>

    [HttpPost]
    public async Task<ActionResult<InmateProfileResponseModel>> Create(InmateProfileRequestModel model)
    {
        await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
        var result = await _inmateProfileService.CreateAsync(model, CurrentUser.UserId);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Update an inmate profile    
    /// </summary>
    /// <param name="id">The id of the inmate profile</param>
    /// <param name="model">The inmate profile request model <see cref="InmateProfileRequestModel"/></param>
    /// <returns>The inmate profile response model <see cref="InmateProfileResponseModel"/></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<InmateProfileResponseModel>> Update(Guid id, InmateProfileRequestModel model)
    {
        await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
        var result = await _inmateProfileService.UpdateAsync(id, model, "Hello");
        return Ok(result);
    }


    /// <summary>
    /// Delete an inmate profile    
    /// </summary>
    /// <param name="id">The id of the inmate profile</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _inmateProfileService.DeleteAsync(id, "Hello");
        return NoContent();
    }

    /// <summary>
    /// Get an inmate profile by id
    /// </summary>
    /// <param name="id">The id of the inmate profile</param>
    /// <returns>The inmate profile response model <see cref="InmateProfileResponseModel"/></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<InmateProfileResponseModel>> GetById(Guid id)
    {
        var result = await _inmateProfileService.GetByIdAsync(id, "Hello");
        return result == null ? NotFound() : Ok(result);
    }
}
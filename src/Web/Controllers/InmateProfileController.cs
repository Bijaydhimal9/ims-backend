using Application.Common.Interfaces;
using Application.Common.Models.RequestModels;
using Application.Common.Models.ResponseModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class InmateProfileController : BaseApiController
    {
        private readonly IInmateProfileService _inmateProfileService;
        private readonly IValidator<InmateProfileRequestModel> _validator;
        public InmateProfileController(IInmateProfileService inmateProfileService,
        IValidator<InmateProfileRequestModel> validator)
        {
            _inmateProfileService = inmateProfileService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<InmateProfileResponseModel>> Create(InmateProfileRequestModel model)
        {
            await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
            var result = await _inmateProfileService.CreateAsync(model, CurrentUser.UserId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InmateProfileResponseModel>> Update(Guid id, InmateProfileRequestModel model)
        {
            await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
            var result = await _inmateProfileService.UpdateAsync(id, model, "Hello");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _inmateProfileService.DeleteAsync(id, "Hello");
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InmateProfileResponseModel>> GetById(Guid id)
        {
            var result = await _inmateProfileService.GetByIdAsync(id, "Hello");
            return result == null ? NotFound() : Ok(result);
        }
    }
}
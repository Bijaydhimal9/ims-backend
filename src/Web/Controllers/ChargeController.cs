using Application.Common.Interfaces;
using Application.Common.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
public class ChargeController : BaseApiController
{
    private readonly IChargeService _chargeService;
    public ChargeController(IChargeService chargeService)
    {
        _chargeService = chargeService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<ChargeResponseModel>>> GetAll()
    {
        var charges = await _chargeService.GetChargesAsync();
        return Ok(charges);
    }
}
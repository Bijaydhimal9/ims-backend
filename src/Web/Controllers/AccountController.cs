using Application.Common.Interfaces;
using Application.Common.Models.RequestModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AccountController : BaseApiController
{
    private readonly IValidator<LoginRequestModel> _validator;
    private readonly IUserService _userService;
    public AccountController(IValidator<LoginRequestModel> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel model)
    {
        await _validator.ValidateAsync(model, options => options.ThrowOnFailures()).ConfigureAwait(false);
        var result = await _userService.VerifyUserAndGetToken(model).ConfigureAwait(false);
        return Ok(result);
    }
}
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers;
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseApiController : ControllerBase
{
    private CurrentUser _currentUser;

    protected CurrentUser CurrentUser
    {
        get
        {
            if (_currentUser == null && User?.Identity?.IsAuthenticated == true)
            {
                _currentUser = User.ToLoggedInUser();
            }
            return _currentUser;
        }
    }

    protected BaseApiController() { }
}
using System.Security.Claims;
using Application.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web.Common;
public static class Util
{

    public static readonly JsonSerializerSettings SerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        DateFormatString = "MM/dd/yyyy HH:mm:ss",
        DateTimeZoneHandling = DateTimeZoneHandling.Utc
    };

    public static string Serialize<T>(T value)
    {
        return JsonConvert.SerializeObject(value, SerializerSettings);
    }

    public static T? Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value, SerializerSettings);
    }

    public static CurrentUser ToLoggedInUser(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.GetClaim("uid", isRequired: true);
        var email = claimsPrincipal.GetClaim(ClaimTypes.Email, isRequired: false);
        return new CurrentUser { UserId = userId, Email = email };
    }

    public static string? GetClaim(this ClaimsPrincipal claimsPrincipal, string claimType, bool isRequired = false)
    {
        var claim = FindClaim(claimsPrincipal, claimType, isRequired);
        return claim?.Value;
    }

    public static T? GetClaim<T>(this ClaimsPrincipal claimsPrincipal, string claimType, bool isRequired = false)
    {
        var claim = FindClaim(claimsPrincipal, claimType, isRequired);
        if (claim == null)
        {
            return default;
        }
        return Deserialize<T>(claim.Value);
    }

    private static Claim? FindClaim(ClaimsPrincipal claimsPrincipal, string claimType, bool isRequired)
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
        if (claim == null && isRequired)
        {
            throw new ServiceException($"'{claimType}' claim type is missing.");
        }

        return claim;
    }
}
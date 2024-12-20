using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.RequestModels;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;
public class UserService : BaseService, IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    private readonly JWT _jwt;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signinManager;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
     IOptions<JWT> jwt) : base(unitOfWork, logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userManager = userManager;
        _signinManager = signInManager;
        _jwt = jwt.Value;

    }

    public async Task<AuthenticationModel> VerifyUserAndGetToken(LoginRequestModel model)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email.ToLower());
            if (user == null)
            {
                throw new ForbiddenException("Invalid email or password");
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                throw new ForbiddenException("Invalid email or password");
            }

            var authenticationModel = new AuthenticationModel();
            var userModel = new UserModel
            {
                Email = user.Email,
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.User = userModel;

            return authenticationModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex is ServiceException ? ex : new ServiceException(ex.Message);
        }
    }


    #region Private Methods
    /// <summary>
    /// Handle to create jwt token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
            };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);

        await Task.CompletedTask;
        return jwtSecurityToken;
    }

    #endregion
}
using Application.Common.Dtos;
using Application.Common.Models.RequestModels;

namespace Application.Common.Interfaces;
public interface IUserService
{
    Task<AuthenticationModel> VerifyUserAndGetToken(LoginRequestModel model);
}
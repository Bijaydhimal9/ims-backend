using Application.Common.Models.ResponseModels;

namespace Application.Common.Interfaces;
public interface IChargeService
{
    Task<IList<ChargeResponseModel>> GetChargesAsync();
}
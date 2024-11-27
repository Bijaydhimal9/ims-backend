using Application.Common.Interfaces;
using Application.Common.Models.ResponseModels;
using Infrastructure.Common;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;
public class ChargeService : BaseService, IChargeService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ILogger _logger;

    public ChargeService(IUnitOfWork unitOfWork, ILogger<ChargeService> logger) : base(unitOfWork, logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IList<ChargeResponseModel>> GetChargesAsync()
    {
        return await ExecuteWithResultAsync(async () =>
        {
            var charges = await _unitOfWork.DbContext.Charges.ToListAsync();
            return charges.Adapt<IList<ChargeResponseModel>>();
        });
    }
}
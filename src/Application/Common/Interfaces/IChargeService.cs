using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models.ResponseModels;

namespace Application.Common.Interfaces
{
    public interface IChargeService
    {
        Task<IList<ChargeResponseModel>> GetChargesAsync();
    }
}
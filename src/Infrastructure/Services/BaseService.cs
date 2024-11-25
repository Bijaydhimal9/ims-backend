using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public abstract class BaseService
    {

        protected readonly IUnitOfWork _unitOfWork;

        protected readonly ILogger _logger;

        protected BaseService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        protected TResult ExecuteWithResult<TResult>(Func<TResult> delegateFunc)
        {
            try
            {
                return delegateFunc();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                throw ex is ServiceException ? ex : new ServiceException(ex.Message);
            }
        }

        protected async Task<TResult> ExecuteWithResultAsync<TResult>(Func<Task<TResult>> delegateFunc)
        {
            try
            {
                return await delegateFunc().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                throw ex is ServiceException ? ex : new ServiceException(ex.Message);
            }
        }

        protected async Task Execute(Func<Task> delegateFunc)
        {
            try
            {
                await delegateFunc().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                throw ex is ServiceException ? ex : new ServiceException(ex.Message);
            }
        }

        protected async Task ExecuteAsync(Func<Task> delegateFunc)
        {
            try
            {
                await delegateFunc().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message, ex);
                throw ex is ServiceException ? ex : new ServiceException(ex.Message);
            }
        }
    }
}
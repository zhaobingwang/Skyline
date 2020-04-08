using Blocks.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Blocks.Infrastructure.Core.Behaviors
{
    public class TransactionBehavior<TDbContext, TReqeust, TResponse> : IPipelineBehavior<TReqeust, TResponse>
        where TDbContext : EFContext
    {
        ILogger _logger;
        TDbContext _dbContext;
        public TransactionBehavior(TDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<TResponse> Handle(TReqeust request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("开始事务：{TransactionId} {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("提交事务：{TransactionId} {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务发生错误：{CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}

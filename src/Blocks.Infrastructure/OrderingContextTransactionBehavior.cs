using Blocks.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;

namespace Blocks.Infrastructure
{
    public class OrderingContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<OrderingContext, TRequest, TResponse>
    {
        public OrderingContextTransactionBehavior(OrderingContext dbContext, ILogger<OrderingContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, logger)
        {
        }
    }
}

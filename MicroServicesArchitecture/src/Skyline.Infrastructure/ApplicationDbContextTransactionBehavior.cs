using Microsoft.Extensions.Logging;
using Skyline.Infrastructure.Core.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure
{
    public class ApplicationDbContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<ApplicationDbContext, TRequest, TResponse>
    {
        public ApplicationDbContextTransactionBehavior(ApplicationDbContext dbContext, ILogger<ApplicationDbContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, logger)
        {
        }
    }
}

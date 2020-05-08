using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(IWebHostEnvironment hostEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ILogger<GlobalExceptionFilter> logger)
        {
            _hostEnvironment = hostEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (_hostEnvironment.IsDevelopment())
            {
                var result = new ViewResult { ViewName = "CustomError" };
                result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                                            context.ModelState);
                result.ViewData.Add("Exception", context.Exception);
            }
            else
            {
                // TODO: friendly exception message
                if (context.Exception is UserOperationException)
                {
                }
            }
            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}

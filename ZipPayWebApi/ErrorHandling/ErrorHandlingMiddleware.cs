using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using ZipPayWebApp.BAL.Exceptions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace ZipPayWebApp.ErrorHandling
{
    public interface IErrorHandlingMiddleware : IMiddleware
    {

    }
    public class ErrorHandlingMiddleware : IErrorHandlingMiddleware
    {
        private readonly IActionResultExecutor<ObjectResult> _actionResultExecutor;
      
        public ErrorHandlingMiddleware(IActionResultExecutor<ObjectResult> actionResultExecutor)
        {
            _actionResultExecutor = actionResultExecutor;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorStatusCode = HttpStatusCode.InternalServerError;
                if (ex is BadRequestException)
                    errorStatusCode = HttpStatusCode.BadRequest;
                else if(ex is UnauthorizedAccessException)
                    errorStatusCode = HttpStatusCode.Unauthorized;

                context.Response.StatusCode = (int)errorStatusCode;
                context.Response.ContentType = "application/json";


                var routeData = context.GetRouteData();
                var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

                await _actionResultExecutor.ExecuteAsync(actionContext, new ObjectResult(new { errorCode = ex.HResult, errorMessage = ex.Message }));

            }
        }
    }
}

using CustomerLibraryAPI.Common;
using CustomerLibraryAPI.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerLibraryAPI.WebApp.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new ErrorModel
                {
                    Title = context.Exception.Message,
                    Status = StatusCodes.Status404NotFound
                });
            }
            else if (context.Exception is NotDeletedException)
            {
                context.Result = new ObjectResult(new ErrorModel
                {
                    Title = context.Exception.Message,
                    Status = StatusCodes.Status405MethodNotAllowed
                })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
            else
            {
                context.Result = new ObjectResult(new ErrorModel
                {
                    Title = "Something went wrong.",
                    Status = StatusCodes.Status500InternalServerError
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            base.OnException(context);
        }
    }
}
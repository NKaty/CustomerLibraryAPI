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
            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                context.Result = new NotFoundObjectResult(new ErrorModel
                {
                    Title = context.Exception.Message,
                    Status = StatusCodes.Status404NotFound
                });
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
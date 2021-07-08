using CustomerLibraryAPI.Common;
using CustomerLibraryAPI.WebApp.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.FilterTests
{
    public class ExceptionFilterTests
    {
        [Fact]
        public void ShouldReturn405StatusCode()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new NotDeletedException()
            };

            var filter = new ExceptionFilter();

            filter.OnException(exceptionContext);

            Assert.Equal(405, (exceptionContext.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public void ShouldReturn404StatusCode()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new NotFoundException()
            };

            var filter = new ExceptionFilter();

            filter.OnException(exceptionContext);

            Assert.Equal(404, (exceptionContext.Result as ObjectResult)?.StatusCode);
        }

        [Fact]
        public void ShouldReturn500StatusCode()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception()
            };

            var filter = new ExceptionFilter();

            filter.OnException(exceptionContext);

            Assert.Equal(500, (exceptionContext.Result as ObjectResult)?.StatusCode);
        }
    }
}

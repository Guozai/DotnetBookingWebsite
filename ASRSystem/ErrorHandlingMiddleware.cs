using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Asr
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == 404) {
                await httpContext.Response.WriteAsync("I CAN'T FIND THE PAGE!");
            }
            else if (httpContext.Response.StatusCode == 403) {
                await httpContext.Response.WriteAsync("I DON'T HAVE ACCESS RIGHT TO THE PAGE!");
            }
            else if (httpContext.Response.StatusCode == 401) {
                await httpContext.Response.WriteAsync("I AM NOT AUTHORIZED TO VIEW THE PAGE!");
            }
            else if (httpContext.Response.StatusCode == 400) {
                await httpContext.Response.WriteAsync("I AM USING THE WRONG FORMAT WHEN COMMUNICATING WITH THE SERVER!");
            }
            else if (httpContext.Response.StatusCode == 500) {
                await httpContext.Response.WriteAsync("SERVER SIDE IS HAVING AN ERROR!");
            }
            else {
                await httpContext.Response.WriteAsync("WOOPS, I MEET AN UNKNOWN ERROR!");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

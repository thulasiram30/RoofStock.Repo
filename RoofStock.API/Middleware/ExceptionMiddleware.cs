using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RoofStock.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is DbUpdateException)
                await BuildResponse(context, HttpStatusCode.Conflict, new ErrorResponse((int)HttpStatusCode.Conflict, exception.InnerException == null ? exception.Message : exception.InnerException.Message));
            else
                await BuildResponse(context, HttpStatusCode.InternalServerError, new ErrorResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error"));
        }

        private static async Task BuildResponse(HttpContext context, HttpStatusCode statusCode, ErrorResponse error)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}

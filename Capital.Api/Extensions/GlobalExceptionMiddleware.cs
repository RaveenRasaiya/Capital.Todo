using Capital.Application.Exceptions;
using Capital.Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capital.Api.Extensions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            ErrorModel errorModel = new ErrorModel { TraceId = Guid.NewGuid().ToString() };
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(ArgumentNullException) || exceptionType == typeof(ApiException)) // 
            {
                errorModel.Message = exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                errorModel.Message = exception.Message;
            }

            var result = JsonSerializer.Serialize(errorModel);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}

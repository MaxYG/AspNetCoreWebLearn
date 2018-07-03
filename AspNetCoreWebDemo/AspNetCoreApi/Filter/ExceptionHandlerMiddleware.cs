using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspNetCoreApi.Filter
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch (exception)
            {
                case System.Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case ApplicationException e when exceptionType == typeof(ApplicationException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = "Internal Server Error";
                    break;
            }

            var response = new { code = statusCode, message = errorCode };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);

        }
    }

    public abstract class ApplicationException : System.Exception
    {
        public string Code { get; }

        protected ApplicationException()
        {
        }

        public ApplicationException(string code)
        {
            Code = code;
        }

        public ApplicationException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public ApplicationException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public ApplicationException(System.Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public ApplicationException(System.Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}

using System.Net;
using System.Net.Mime;
using System.Text.Json;
using YGKAPI.Application.Exceptions;
using YGKAPI.Application.Features;
using YGKAPI.Infrastructure.Helpers;

namespace YGKAPI.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string headerLang = context.Request.Headers["Language"].ToString();
            string language = (!string.IsNullOrEmpty(headerLang)) ? headerLang : "en";
            BaseResponse<int> response = new()
            {
                Data = -1,
                Code = (Int16)context.Response.StatusCode,
                Error = TranslationHelper.GetErrorMessageByName(exception.Message, language),
                Succeeded = false
            };
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}

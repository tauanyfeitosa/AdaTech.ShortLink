using AdaTech.ShortLink.Service.Exceptions;

namespace AdaTech.ShortLink.WebAPI.Handlers
{
    public class HandlerException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandlerException> _logger;

        public HandlerException(RequestDelegate next, ILogger<HandlerException> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            int originalStatusCode = httpContext.Response.StatusCode;

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Algo de errado aconteceu: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                NotFoundException _ => StatusCodes.Status404NotFound,
                ExpiredDateException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = exception.Message;

            context.Response.StatusCode = statusCode;

            var errorResponse = new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }

    }
}

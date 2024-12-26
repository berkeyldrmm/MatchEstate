using MatchEstate.Wrappers;
using System.Text.Json;

namespace RealEstate.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponse
                {
                    Success = false,
                    Message = e.Message + "\rStatus Code: 500"
                }));
            }
        }
    }
}

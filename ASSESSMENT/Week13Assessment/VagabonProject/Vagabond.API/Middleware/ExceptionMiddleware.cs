//using Vagabond.API.Exceptions;

//namespace Vagabond.API.Middleware
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public ExceptionMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (DestinationNotFoundException ex)
//            {
//                context.Response.StatusCode = 404;

//                await context.Response.WriteAsJsonAsync(new
//                {
//                    StatusCode = 404,
//                    Message = ex.Message
//                });
//            }
//            catch (Exception ex)
//            {
//                context.Response.StatusCode = 500;

//                await context.Response.WriteAsJsonAsync(new
//                {
//                    StatusCode = 500,
//                    Message = ex.Message
//                });
//            }
//        }
//    }
//}


//csharp Vagabond.API\Middleware\ExceptionMiddleware.cs
using Vagabond.API.Exceptions;
using Microsoft.Extensions.Hosting;

namespace Vagabond.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DestinationNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { StatusCode = 404, Message = ex.Message });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                var response = new
                {
                    StatusCode = 500,
                    Message = _env.IsDevelopment() ? ex.Message : "An unexpected error occurred.",
                    Inner = _env.IsDevelopment() ? ex.InnerException?.Message : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
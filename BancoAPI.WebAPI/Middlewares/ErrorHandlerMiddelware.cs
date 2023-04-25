using BancoAPI.Application.Wrappers;
using System.Text.Json;

namespace BancoAPI.WebAPI.Middlewares
{
    public class ErrorHandlerMiddelware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddelware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContent)
        {
            try
            {
                await _next(httpContent);
            }
            catch (Exception ex)
            {
                var response = httpContent.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = ex?.Message };

                switch (ex)
                {
                    case Application.Exceptions.ApiException:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                    case Application.Exceptions.ValidationException e:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using PIM.Entities;
using System.Net.Http;
using Microsoft.Extensions.Logging;


namespace PIM_Middleware
{
    public class PIMMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly Microsoft.Extensions.Logging.ILogger _logger;
            // private readonly ITokenService _tokenService;
            public PIMMiddleware(RequestDelegate next, ILoggerFactory logFactory)
            {
                _next = next;
                _logger = logFactory.CreateLogger("PIMMiddleware");
                // _tokenService = tokenService;
            }

            public async Task Invoke(HttpContext httpContext)
            {
                try
                {
                    //To Handle : handle each validation of a data annotation return code 2 - Input Validation Failed

                    //Get the request path
                    var path = JsonConvert.SerializeObject(httpContext.Request.Path.Value ?? "");

                    //Get the headers
                    var headers = JsonConvert.SerializeObject(httpContext.Request.Headers);

                    //Get the Request Body - to be continued
                    //Stream stream = httpContext.Request.Body;
                    //httpContext.Response.Body = new MemoryStream();
                    //string _originalContent = new StreamReader(stream).ReadToEnd();

                    var body = "";
                    _logger.LogInformation($"new hit : {path}, headers : {headers}, body : {body}");

                    // if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                    // {
                    // var token = authHeaderValue.ToString().Replace("Bearer ", "");
                    //var isValid = _tokenService.ValidateToken(token);

                    // if (!isValid)
                    //
                    //else
                    //{
                    //    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //    await httpContext.Response.WriteAsync("Missing token");
                    //    return;
                    //}

                    await _next(httpContext);
                    //Get the Response Body - to be continued
                    //_logger.LogInformation($"response : {httpContext.Response.Body}");

                }
                catch (Exception ex)
                {
                    // Log the error
                    _logger.LogError($"An error occurred in MyMiddleware: {ex}");
                    GlobalResponse errorResp = new();
                    errorResp.statusCode = 1;
                    errorResp.message = "Something went wrong. Please try again later ! ";

                    // Serialize error response to JSON
                    var jsonResponse = JsonConvert.SerializeObject(errorResp);

                    // Set content type header
                    httpContext.Response.ContentType = "application/json";

                    // Write JSON response to the response stream
                    await httpContext.Response.WriteAsync(jsonResponse);

                    //Another way
                    //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    //await httpContext.Response.WriteAsync("An unexpected error occurred. Please try again later.");
                }
            }
        }
        // Extension method used to add the middleware to the HTTP request pipeline.
        public static class PIMMiddlewareExtensions
        {
            public static IApplicationBuilder UsePIMMiddleware(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<PIMMiddleware>();
            }
        }

    }

using System.Net;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using API.Errors;

namespace API.Middleware
{
    
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next; 
       private readonly ILogger<ExceptionMiddleware> _logger;
       private readonly IHostEnvironment _evn;

       public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment evn )
       {
           this._next = next;
           this._logger = logger;
           this._evn = evn;
       }

       public async Task InvokeAsync(HttpContext context){
           try
           {
               await _next(context);
           }
           catch (Exception ex)
           {
                _logger.LogError(ex, ex.Message);

                context.Request.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _evn.IsDevelopment()
                ? new apiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                :new apiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var Json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(Json);

           }
       }
    }
}
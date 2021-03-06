using GoalSystem.Inventory.Api.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Api.Middlewares
{
    /// <summary>
    /// Middleware to handler the ApiKey in the headers
    /// </summary>
    public class ApiKeySecretAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeySecretAuthorizationMiddleware> _logger;
        private readonly ApiKeySettings _apiKeySettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="configuration">Configuration of ApiKey (name and secret)</param>
        public ApiKeySecretAuthorizationMiddleware(
            RequestDelegate next,
            ILogger<ApiKeySecretAuthorizationMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;

            _apiKeySettings = new ApiKeySettings();
            configuration.GetSection("ApiKey").Bind(_apiKeySettings);
        }

        /// <summary>
        /// Check if in the header is the ApiKey
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(
                _apiKeySettings.Name, 
                out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                var remoteIp = context.Connection.RemoteIpAddress;
                var error = $"Request from Remote IP address: {remoteIp} without Header correct. Api Key was not provided";
                _logger.LogInformation(error);               
                await context.Response.WriteAsync(error);

                return;
            }

            if (!_apiKeySettings.Secret.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware)");
                return;
            }

            await _next(context);
        }
    }
}

using GoalSystem.Inventory.Api.Configuration;
using GoalSystem.Inventory.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace GoalSystem.Inventory.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerWithApiKeySecurity(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var apiKeySettings = new ApiKeySettings();
            configuration.GetSection("ApiKey").Bind(apiKeySettings); 

            if (string.IsNullOrEmpty(apiKeySettings?.Name)) 
                throw new BusinessException("ApiKey.Name is null or empty.");

            services.AddSwaggerGen(c =>
            {
                const string securityDefinition = "ApiKey";

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Goal System Inventory API",
                    Description = "Inventory API"
                });
                c.AddSecurityDefinition(securityDefinition, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = apiKeySettings.Name,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = apiKeySettings.Name,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = securityDefinition }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}

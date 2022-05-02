using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;

namespace GoalSystem.Inventory.Api.Configuration
{
    /// <summary>
    /// Static Class that handler of related with the configuration of SeriLog
    /// </summary>
    public static class SerilogConfig
    {
        /// <summary>
        /// Create all services in order to handler the logs with Serilog
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder CreateLoggerSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                            .ReadFrom.Configuration(hostingContext.Configuration)
                            //.TryAddApplicationInsights()
                            //.AddFilters()
            );

            return hostBuilder;
        }

        /// <summary>
        /// If we want handler in this class AppInsight
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <returns></returns>
        private static LoggerConfiguration TryAddApplicationInsights(this LoggerConfiguration loggerConfiguration)
        {
            var appInsightsTelemetryConfiguration = TelemetryConfiguration.CreateDefault();
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var instrumentationKey = configuration.GetSection("ApplicationInsights:InstrumentationKey").Value;
            appInsightsTelemetryConfiguration.InstrumentationKey = instrumentationKey;

            if (!string.IsNullOrEmpty(instrumentationKey))
            {
                loggerConfiguration
                    .WriteTo.ApplicationInsights(
                                appInsightsTelemetryConfiguration,
                                TelemetryConverter.Traces);
            }

            return loggerConfiguration;
        }

        /// <summary>
        /// If we want add filter to Serilog in order to avoid certains messages
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <returns></returns>
        private static LoggerConfiguration AddFilters(this LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("health")))
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")));

            return loggerConfiguration;
        }
    }
}

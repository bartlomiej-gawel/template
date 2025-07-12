using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Template.Aspire.ServiceDefaults.Extensions;

internal static class SerilogExtensions
{
    public static IHostApplicationBuilder AddSerilogConfiguration(this IHostApplicationBuilder builder)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();

        var openTelemetryExporter = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
        if (openTelemetryExporter is not null)
        {
            loggerConfiguration.WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = openTelemetryExporter;
            });
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        builder.Services.AddSerilog();
        builder.Logging.ClearProviders().AddSerilog();

        return builder;
    }
}
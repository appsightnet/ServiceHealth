using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add healthchecks to the container.

builder.Services
    .AddOptions<HealthChecksUrlGroupOptions>()
    .Bind(builder.Configuration.GetSection("HealthChecksUrlGroupOptions"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var urlGroupOptions = builder.Configuration
    .GetSection("HealthChecksUrlGroupOptions")
    .Get<HealthChecksUrlGroupOptions>();

var healthChecksBuilder = builder.Services.AddHealthChecks();

foreach (var urlGroup in urlGroupOptions.UrlGroups)
{
    healthChecksBuilder.AddUrlGroup(
        uris: urlGroup.Urls,
        httpMethod: urlGroup.HttpMethod,
        name: urlGroup.Name,
        failureStatus: urlGroup.FailureStatus,
        tags: urlGroup.Tags
    );
}

// Add other services to the container.

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapHealthChecks(
    "/minigames/healthz/liveness",
    new HealthCheckOptions
    {
        Predicate = healthCheck =>
            healthCheck.Tags.Contains("minigames") && healthCheck.Tags.Contains("liveness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.MapHealthChecks(
    "/webapps/healthz/liveness",
    new HealthCheckOptions
    {
        Predicate = healthCheck =>
            healthCheck.Tags.Contains("webapps") && healthCheck.Tags.Contains("liveness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.MapHealthChecks(
    "/webapps/healthz/readiness",
    new HealthCheckOptions
    {
        Predicate = healthCheck =>
            healthCheck.Tags.Contains("webapps") && healthCheck.Tags.Contains("readiness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.MapHealthChecks(
    "/websites/healthz/liveness",
    new HealthCheckOptions
    {
        Predicate = healthCheck =>
            healthCheck.Tags.Contains("websites") && healthCheck.Tags.Contains("liveness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.MapGet(
    "/",
    () =>
    {
        return "alive!";
    }
);

app.UseRouting().UseEndpoints(config => config.MapHealthChecksUI());

app.Run();

# Overview
This repository is the health checks service for https://appsight.net .

This project implements the following:
- Implement basic HealthChecks/HealthChecksUI in .NET 6.0.
- Customize CSS for HealthChecksUI.
- Define `IServiceCollection.AddHealthChecks().AddUrlGroup()` in `appsettings.json`.
- Run `ValidateDataAnnotations()` for nested IOptions on startup.
- GitHub Actions workflow for injecting sensitive information into Azure Web Apps
etc...

Currently, this repository is intentionally public. I hope it will be useful to someone.

# License
MIT

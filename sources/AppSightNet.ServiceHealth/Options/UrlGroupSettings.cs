using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

public class UrlGroupSettings
{
    [Required]
    [MinLength(1)]
    public Uri[] Urls { get; set; } = default!;

    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    public string? Name { get; set; }

    public HealthStatus? FailureStatus { get; set; }

    public string[]? Tags { get; set; }
}

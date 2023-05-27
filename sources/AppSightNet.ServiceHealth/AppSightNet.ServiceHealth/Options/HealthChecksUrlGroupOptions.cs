using System.ComponentModel.DataAnnotations;

public class HealthChecksUrlGroupOptions : IValidatableObject
{
    public UrlGroupSettings[] UrlGroups { get; set; } = new UrlGroupSettings[] { };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();

        if (UrlGroups != null)
        {
            foreach (var urlGroup in UrlGroups)
            {
                var context = new ValidationContext(urlGroup, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(
                    urlGroup,
                    context,
                    results,
                    validateAllProperties: true
                );

                if (!isValid)
                {
                    validationResults.AddRange(results);
                }
            }
        }

        return validationResults;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessLayer.DTO;

public class TechnicalSpecificationsValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("Technical Specifications is required");
        }

        var str = value.ToString()!;

        // Check length > 10
        if (str.Length <= 10)
        {
            return new ValidationResult("Technical Specifications must be greater than 10 characters");
        }

        // Check for forbidden characters: #, @, &, (, )
        if (str.Contains('#') || str.Contains('@') || str.Contains('&') || str.Contains('(') || str.Contains(')'))
        {
            return new ValidationResult("Technical Specifications cannot contain special characters: #, @, &, (, )");
        }

        // Check each word starts with capital letter or number
        var words = str.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var pattern = @"^[A-Z0-9][a-zA-Z0-9]*$";
        
        foreach (var word in words)
        {
            if (!Regex.IsMatch(word, pattern))
            {
                return new ValidationResult("Each word in Technical Specifications must begin with a capital letter or number");
            }
        }

        return ValidationResult.Success;
    }
}


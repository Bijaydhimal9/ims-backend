using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Common.Models.RequestModels;

/// <summary>
/// Represents a request model for a login
/// </summary>
public class LoginRequestModel : IValidatableObject
{
    /// <summary>
    /// Gets or sets the email of the user
    /// </summary>
    public string Email { get; set; } = null!;
    /// <summary>
    /// Gets or sets the password of the user
    /// </summary>
    public string Password { get; set; } = null!;
    /// <summary>
    /// Gets or sets a value indicating whether the email is valid
    /// </summary>
    public bool IsEmail { get; private set; } = false;

    /// <summary>
    /// Validates the login request model
    /// </summary>
    /// <param name="validationContext">The validation context</param>
    /// <returns>A collection of validation results</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        //Validate email format
        const string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        if (new Regex(emailRegex).IsMatch(Email))
        {
            IsEmail = true;
        }
        else
        {
            result.Add(new ValidationResult("Invalid Email", new[] { nameof(Email) }));
        }

        return result;
    }
}

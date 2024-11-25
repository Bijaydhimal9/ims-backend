using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Common.Models.RequestModels
{
    public class LoginRequestModel : IValidatableObject
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsEmail { get; private set; } = false;

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
}
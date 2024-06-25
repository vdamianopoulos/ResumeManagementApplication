using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ResumeManagementApplication.Shared.Abstractions;
using ResumeManagementApplication.Shared.Models;

namespace ResumeManagementApplication.Shared.Validations;

public partial class ModelsValidationLogic
{
    [GeneratedRegex(@"^\d{10}$")]
    private static partial Regex Mobile();

    [GeneratedRegex(@"^([a-zA-Z]{3,100})$")]
    private static partial Regex Name();

    [GeneratedRegex(@"^([a-zA-Z]{5,100})$")]
    private static partial Regex DegreeName();

    private static bool IsValueValid(Func<Regex> methodName, string val)
    {
        Regex regex = methodName?.Invoke();
        return regex?.IsMatch(val) ?? false;
    }

    public partial class MobileValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string property && !IsValueValid(Mobile, property))
            {
                return new ValidationResult("Invalid mobile format.");
            }
            return ValidationResult.Success;
        }
        public ValidationResult? GetIsValidWithMessage(object? value)
        {
            return IsValid(value, null);
        }
    }
    public partial class NameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string property && !IsValueValid(Name, property))
            {
                return new ValidationResult("Invalid format.");
            }
            return ValidationResult.Success;
        }
        public ValidationResult? GetIsValidWithMessage(object? value)
        {
            return IsValid(value, null);
        }
    }
    public partial class DegreeNameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string property && !IsValueValid(DegreeName, property))
            {
                return new ValidationResult("Invalid format.");
            }
            return ValidationResult.Success;
        }
        public ValidationResult? GetIsValidWithMessage(object? value)
        {
            return IsValid(value, null);
        }
    }
    public class ModelValidator() : IValidator
    {
        public bool IsValid(Candidate candidate)
        {
            var validations = new List<bool>()
            {
                IsValidFirstName(candidate),
                IsValidLastName(candidate),
                IsValidMobile(candidate),
                IsValidEmail(candidate)
            };

            return validations.All(x => x);
        }
        public bool IsValid(Degree degree)
        {
            var validations = new List<bool>()
            {
                IsValidDegreeName(degree)
            };

            return validations.All(x => x);
        }

        public bool IsValidFirstName(Candidate candidate) => (!string.IsNullOrEmpty(candidate.FirstName) && new NameValidation().GetIsValidWithMessage(candidate.FirstName) == ValidationResult.Success);
        public bool IsValidLastName(Candidate candidate) => (!string.IsNullOrEmpty(candidate.LastName) && new NameValidation().GetIsValidWithMessage(candidate.LastName) == ValidationResult.Success);
        public bool IsValidMobile(Candidate candidate) => (!string.IsNullOrEmpty(candidate.Mobile) && new MobileValidation().GetIsValidWithMessage(candidate.Mobile) == ValidationResult.Success);
        public bool IsValidEmail(Candidate candidate) => (!string.IsNullOrEmpty(candidate.Email) && new EmailAddressAttribute().IsValid(candidate.Email));
        public bool IsValidDegreeName(Degree degree) => (!string.IsNullOrEmpty(degree.Name) && new DegreeNameValidation().GetIsValidWithMessage(degree.Name) == ValidationResult.Success);
    }
}
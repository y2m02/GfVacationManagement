using System.ComponentModel.DataAnnotations;
using VacationManagerApi.Models.Requests;

namespace VacationManagerApi.Helpers.CustomValidations
{
    public class ToMustBeGreaterOrEqualToFrom : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vacation = (VacationRequest)validationContext.ObjectInstance;

            return vacation.To.Date < vacation.From.Date
                ? new ValidationResult(
                    $"{validationContext.MemberName} must be greater of equal to {nameof(vacation.From)}"
                )
                : ValidationResult.Success;
        }
    }
}

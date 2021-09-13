using System.ComponentModel.DataAnnotations;
using VacationManagerApi.Models.Requests;

namespace VacationManagerApi.Helpers.CustomValidations
{
    public class HolidayIdMustBeGreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vacation = (VacationRequest)validationContext.ObjectInstance;

            return vacation.HolidayId < 1
                ? new ValidationResult($"{validationContext.MemberName} must be greater than 0")
                : ValidationResult.Success;
        }
    }
}

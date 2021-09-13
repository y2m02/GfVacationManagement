using System.ComponentModel.DataAnnotations;
using VacationManagerApi.Helpers.CustomValidations;

namespace VacationManagerApi.Models.Requests
{
    public class HolidayRequest : IRequest
    {
        [Required]
        [StringLength(50)]
        [HolidayIdMustBeGreaterThanZero]
        public string Name { get; set; }

        [Range(1, 10)]
        public int TotalDays { get; set; }
    }
}

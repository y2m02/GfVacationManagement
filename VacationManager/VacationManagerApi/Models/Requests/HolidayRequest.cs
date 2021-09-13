using System.ComponentModel.DataAnnotations;

namespace VacationManagerApi.Models.Requests
{
    public class HolidayRequest : IRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int TotalDays { get; set; }
    }
}

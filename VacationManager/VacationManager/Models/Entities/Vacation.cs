using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManager.Models.Entities
{
    public class Vacation : BaseEntity
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        [Range(2020, 2030)]
        public int Year { get; set; }

        [Required]
        public int HolidayId { get; set; }

        [ForeignKey(nameof(HolidayId))]
        public Holiday Holiday { get; set; }
    }
}

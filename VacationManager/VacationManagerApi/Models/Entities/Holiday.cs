using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VacationManagerApi.Models.Entities
{
    public class Holiday : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public int TotalDays { get; set; }

        public ICollection<Vacation> Vacations { get; set; }
    }
}

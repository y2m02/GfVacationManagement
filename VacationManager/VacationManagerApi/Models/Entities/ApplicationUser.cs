using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VacationManagerApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
    }
}

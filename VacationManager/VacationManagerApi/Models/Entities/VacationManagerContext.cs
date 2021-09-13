using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VacationManagerApi.Models.Entities
{
    public class VacationManagerContext : IdentityDbContext<ApplicationUser>
    {
        public VacationManagerContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}

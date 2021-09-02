using Microsoft.EntityFrameworkCore;

namespace VacationManager.Models.Entities
{
    public class VacationManagerContext : DbContext
    {
        public VacationManagerContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}

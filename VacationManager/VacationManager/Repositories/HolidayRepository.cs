using System.Threading.Tasks;
using VacationManager.Models.Entities;

namespace VacationManager.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public Task Update(Holiday entity) => Update(entity, new[] { "Name", "TotalDays" });
    }
}

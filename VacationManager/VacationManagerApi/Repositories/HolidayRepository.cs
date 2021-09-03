using System.Threading.Tasks;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public Task Update(Holiday entity) => Update(entity, new[] { "Name", "TotalDays" });
    }
}

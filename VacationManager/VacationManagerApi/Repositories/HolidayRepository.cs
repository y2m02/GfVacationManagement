using System.Threading.Tasks;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public async Task<Holiday> Update(Holiday entity)
        {
            await Update(entity, new[] { "Name", "TotalDays" }).ConfigureAwait(false);

            return await GetById(entity.Id).ConfigureAwait(false);
        }
    }
}

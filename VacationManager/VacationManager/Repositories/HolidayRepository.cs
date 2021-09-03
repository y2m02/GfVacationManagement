using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManager.Models.Entities;

namespace VacationManager.Repositories
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

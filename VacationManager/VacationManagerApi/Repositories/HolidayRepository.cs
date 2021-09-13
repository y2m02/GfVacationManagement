#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public virtual Task<List<Holiday>> GetAll(int pageNumber, int pageSize)
        {
            var limit = (pageNumber - 1) * pageSize;

            return context.Holidays
                .Skip(limit)
                .Take(pageSize > 100 ? 100 : pageSize)
                .ToListAsync();
        }

        public async Task<Holiday?> GetById(int id)
        {
            return await context.Holidays
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }
    }
}

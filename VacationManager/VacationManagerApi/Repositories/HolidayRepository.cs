#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public virtual Task<List<Holiday>> GetAll() => context.Set<Holiday>().ToListAsync();

        public async Task<Holiday?> GetById(int id)
        {
            return await context
                .Set<Holiday>()
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }
    }
}

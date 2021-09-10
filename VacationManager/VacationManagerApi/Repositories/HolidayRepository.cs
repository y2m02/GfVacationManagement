using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IHolidayRepository : IBaseRepository<Holiday> { }

    public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(VacationManagerContext context) : base(context) { }

        public virtual Task<List<Holiday>> GetAll() => context.Set<Holiday>().ToListAsync();

        public Task<Holiday> GetById(int id)
        {
            return context.Set<Holiday>().SingleAsync(x => x.Id == id);
        }
    }
}

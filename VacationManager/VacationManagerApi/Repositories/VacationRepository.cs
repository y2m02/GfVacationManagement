#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IVacationRepository : IBaseRepository<Vacation> { }

    public class VacationRepository : BaseRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(VacationManagerContext context) : base(context) { }

        public Task<List<Vacation>> GetAll(int pageNumber, int pageSize)
        {
            var limit = (pageNumber - 1) * pageSize;

            return context.Vacations
                .Skip(limit)
                .Take(pageSize > 100 ? 100 : pageSize)
                .Include(x => x.Holiday)
                .ToListAsync();
        }

        public async Task<Vacation?> GetById(int id)
        {
            return await context.Vacations
                .Include(x => x.Holiday)
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }
    }
}

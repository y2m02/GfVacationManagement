using System.Threading.Tasks;
using VacationManager.Models.Entities;

namespace VacationManager.Repositories
{
    public interface IVacationRepository : IBaseRepository<Vacation> { }

    public class VacationRepository : BaseRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(VacationManagerContext context) : base(context) { }

        public async Task<Vacation> Update(Vacation entity)
        {
            await Update(entity, new[] { "HolidayId", "Year", "To", "From" }).ConfigureAwait(false);

            return await GetById(entity.Id).ConfigureAwait(false);
        }
    }
}

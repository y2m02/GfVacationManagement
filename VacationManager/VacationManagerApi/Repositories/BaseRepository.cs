using System.Collections.Generic;
using System.Threading.Tasks;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<int> Create(TEntity entity);
        Task<int> Update(TEntity entity);
        Task Delete(TEntity entity);
    }

    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly VacationManagerContext context;

        protected BaseRepository(VacationManagerContext context) => this.context = context;
        
        public async Task<int> Create(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }

        public Task Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);

            return context.SaveChangesAsync();
        }

        protected async Task<int> Update(TEntity entity, IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                context.Entry(entity).Property(propertyName).IsModified = true;
            }

            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }
    }
}

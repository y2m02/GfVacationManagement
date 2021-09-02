using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManager.Models.Entities;

namespace VacationManager.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
    }

    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly VacationManagerContext context;

        public BaseRepository(VacationManagerContext context)
        {
            this.context = context;
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> GetById(int id)
        {
            return context.Set<TEntity>().SingleAsync(x => x.Id == id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public Task Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);

            return context.SaveChangesAsync();
        }

        protected Task Update(TEntity entity, IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                context.Entry(entity).Property(propertyName).IsModified = true;
            }

            return context.SaveChangesAsync();
        }
    }
}

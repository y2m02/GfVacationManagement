using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IBaseService
    {
        Task<IBaseResponse> GetAll();
        Task<IBaseResponse> GetById(int id);
        Task<IBaseResponse> Create(IRequest request);
        Task<IBaseResponse> Update(int id, IRequest request);
        Task<IBaseResponse> Delete(int id);
    }

    public abstract class BaseService<TEntity, TDto>
        where TEntity : BaseEntity, new()
        where TDto : BaseDto
    {
        protected readonly IMapper Mapper;

        protected BaseService(IMapper mapper) => Mapper = mapper;

        protected IBaseRepository<TEntity> Repository { get; set; }

        public Task<IBaseResponse> GetAll()
        {
            return HandleErrors(
                async () =>
                {
                    var entities = await Repository.GetAll().ConfigureAwait(false);

                    return Mapper.Map<List<TDto>>(entities);
                }
            );
        }

        public Task<IBaseResponse> GetById(int id)
        {
            return HandleErrors(
                async () =>
                {
                    var entity = await Repository.GetById(id).ConfigureAwait(false);

                    return Mapper.Map<TDto>(entity);
                }
            );
        }

        public Task<IBaseResponse> Create(IRequest request) => Upsert(request);

        public Task<IBaseResponse> Update(int id, IRequest request) => Upsert(request, id);

        public Task<IBaseResponse> Delete(int id)
        {
            return HandleErrors(() => Repository.Delete(new TEntity { Id = id }));
        }

        protected async Task<IBaseResponse> HandleErrors<TReturn>(Func<Task<TReturn>> executor)
        {
            try
            {
                return new Success(await executor().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }
        }

        protected async Task<IBaseResponse> HandleErrors(Func<Task> executor)
        {
            try
            {
                await executor().ConfigureAwait(false);

                return new Success();
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }
        }

        private Task<IBaseResponse> Upsert(IRequest request, int? id = null)
        {
            return HandleErrors(
                async () =>
                {
                    var entity = Mapper.Map<TEntity>(request);

                    var task = id.HasValue
                        ? Repository.Update(entity.Tap(x => x.Id = id.Value))
                        : Repository.Create(entity);

                    entity = await Repository
                        .GetById(await task.ConfigureAwait(false))
                        .ConfigureAwait(false);

                    return Mapper.Map<TDto>(entity);
                }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Helpers;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IBaseService
    {
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetById(int id);
        Task<BaseResponse> Create(IRequest entity);
        Task<BaseResponse> Update(int id, IRequest entity);
        Task<BaseResponse> Delete(int id);
    }

    public abstract class BaseService<TEntity, TDto>
        where TEntity : BaseEntity, new()
        where TDto : BaseDto
    {
        protected readonly IMapper Mapper;

        protected BaseService(IMapper mapper) => Mapper = mapper;

        protected IBaseRepository<TEntity> Repository { get; set; }

        public Task<BaseResponse> GetAll()
        {
            return HandleErrors(
                async () =>
                {
                    var entities = await Repository.GetAll().ConfigureAwait(false);

                    return new Success(Mapper.Map<List<TDto>>(entities));
                }
            );
        }

        public Task<BaseResponse> GetById(int id)
        {
            return HandleErrors(
                async () =>
                {
                    var entity = await Repository.GetById(id).ConfigureAwait(false);

                    return new Success(Mapper.Map<TDto>(entity));
                }
            );
        }

        public Task<BaseResponse> Create(IRequest entity) => Upsert(entity);

        public Task<BaseResponse> Update(int id, IRequest entity) => Upsert(entity, id);

        public Task<BaseResponse> Delete(int id)
        {
            return HandleErrors(
                async () =>
                {
                    if (id < 1)
                    {
                        return new Validation(new[]
                        {
                            ConsumerMessages.FieldRequired.Format(nameof(id)),
                        });
                    }

                    await Repository.Delete(new TEntity { Id = id }).ConfigureAwait(false);

                    return new Success();
                }
            );
        }

        protected async Task<BaseResponse> HandleErrors(Func<Task<BaseResponse>> executor)
        {
            try
            {
                return await executor().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Failure(new[] { ex.Message });
            }
        }

        private Task<BaseResponse> Upsert(IRequest entity, int? id = null)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Validation(validations);
                    }

                    var mappedEntity = Mapper.Map<TEntity>(entity);

                    var task = id.HasValue
                        ? Repository.Update(mappedEntity.Tap(x => x.Id = id.Value))
                        : Repository.Create(mappedEntity);

                    return new Success(Mapper.Map<TDto>(await task.ConfigureAwait(false)));
                }
            );
        }
    }
}

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
        Task<BaseResponse> Create(IRequest request);
        Task<BaseResponse> Update(int id, IRequest request);
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

        public Task<BaseResponse> Create(IRequest request) => Upsert(request);

        public Task<BaseResponse> Update(int id, IRequest request) => Upsert(request, id);

        public Task<BaseResponse> Delete(int id)
        {
            return HandleErrors(
                async () =>
                {
                    if (id < 1)
                    {
                        return new Validation(
                            new[]
                            {
                                ConsumerMessages.FieldRequired.Format(nameof(id)),
                            }
                        );
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

        private Task<BaseResponse> Upsert(IRequest request, int? id = null)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = request.Validate().ToList();

                    if (validations.Any())
                    {
                        return new Validation(validations);
                    }

                    var entity = Mapper.Map<TEntity>(request);

                    if (id.HasValue)
                    {
                        await Repository
                            .Update(entity.Tap(x => x.Id = id.Value))
                            .ConfigureAwait(false);

                        entity = await Repository.GetById(id.Value).ConfigureAwait(false);
                    }
                    else
                    {
                        await Repository.Create(entity).ConfigureAwait(false);
                    }

                    return new Success(Mapper.Map<TDto>(entity));
                }
            );
        }
    }
}

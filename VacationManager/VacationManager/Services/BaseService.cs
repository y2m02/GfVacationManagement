﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using VacationManager.Models.Entities;
using VacationManager.Models.Helpers;
using VacationManager.Models.Requests;
using VacationManager.Models.Responses;
using VacationManager.Repositories;

namespace VacationManager.Services
{
    public interface IBaseService
    {
        Task<Result> GetAll();
        Task<Result> Create(IRequest entity);
        Task<Result> Update(IUpdateableRequest entity);
        Task<Result> Delete(int id);
    }

    public abstract class BaseService<TEntity, TResponse>
        where TEntity : BaseEntity, new()
        where TResponse : BaseResponse
    {
        protected readonly IMapper Mapper;

        protected BaseService(IMapper mapper) => Mapper = mapper;

        protected IBaseRepository<TEntity> Repository { get; set; }

        public Task<Result> GetAll()
        {
            return HandleErrors(
                async () =>
                {
                    var entities = await Repository.GetAll().ConfigureAwait(false);

                    return new(Mapper.Map<List<TResponse>>(entities));
                }
            );
        }

        public Task<Result> Create(IRequest entity)
        {
            return Upsert(entity, UpsertActionType.Create);
        }

        public Task<Result> Update(IUpdateableRequest entity)
        {
            return Upsert(entity, UpsertActionType.Update);
        }

        public Task<Result> Delete(int id)
        {
            return HandleErrors(
                async () =>
                {
                    if (id < 1)
                    {
                        return new(
                            validationErrors: new[]
                            {
                                ConsumerMessages.FieldRequired.Format(nameof(id)),
                            }
                        );
                    }

                    await Repository.Delete(new TEntity { Id = id }).ConfigureAwait(false);

                    return new(response: ConsumerMessages.SuccessResponse.Format(1, 1, ConsumerMessages.Deleted));
                }
            );
        }

        protected async Task<Result> HandleErrors(Func<Task<Result>> executor)
        {
            try
            {
                return await executor().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new(errorMessage: ex.Message);
            }
        }

        private Task<Result> Upsert(IRequest entity, UpsertActionType actionType)
        {
            return HandleErrors(
                async () =>
                {
                    var validations = entity.Validate().ToList();

                    if (validations.Any())
                    {
                        return new(validationErrors: validations);
                    }

                    var mappedEntity = Mapper.Map<TEntity>(entity);

                    var task = actionType == UpsertActionType.Create
                        ? Repository.Create(mappedEntity)
                        : Repository.Update(mappedEntity);

                    return new(Mapper.Map<TResponse>(await task.ConfigureAwait(false)));
                }
            );
        }
    }
}
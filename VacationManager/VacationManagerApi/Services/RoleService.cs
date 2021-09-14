﻿using System.Linq;
using System.Threading.Tasks;
using VacationManagerApi.Helpers;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IRoleService
    {
        Task<IBaseResponse> GetRoles();
        Task<IBaseResponse> CreateRole(string roleName);
    }

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;

        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public Task<IBaseResponse> GetRoles()
        {
            return ResponseHandler.HandleErrors(
                async () => await repository.GetAll().ConfigureAwait(false)
            );
        }

        public Task<IBaseResponse> CreateRole(string roleName)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var result = await repository.Add(roleName).ConfigureAwait(false);

                    return result.Succeeded
                        ? ResponseHandler.Success(roleName)
                        : ResponseHandler.Validations(result.Errors.Select(x => x.Description));
                }
            );
        }
    }
}

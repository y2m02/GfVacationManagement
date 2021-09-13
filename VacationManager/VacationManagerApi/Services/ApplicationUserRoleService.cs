using System.Linq;
using System.Threading.Tasks;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Services
{
    public interface IApplicationUserRoleService
    {
        Task<IBaseResponse> GetRoles();
        Task<IBaseResponse> CreateRole(string roleName);
    }

    public class ApplicationUserRoleService : IApplicationUserRoleService
    {
        private readonly VacationManagerContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public ApplicationUserRoleService(
            VacationManagerContext context,
            RoleManager<IdentityRole> roleManager
        )
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<IBaseResponse> GetRoles()
        {
            var a = await context.Roles.ToListAsync().ConfigureAwait(false);

            return new Success(a.Select(x => x.Name));
        }

        public async Task<IBaseResponse> CreateRole(string roleName)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            return result.Succeeded
                ? new Success(roleName)
                : new Failure(result.Errors.Join("\n"));
        }
    }
}

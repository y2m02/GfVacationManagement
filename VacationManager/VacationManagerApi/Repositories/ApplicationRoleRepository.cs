using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IApplicationRoleRepository
    {
        Task<List<ApplicationRole>> GetAll();
        Task<IdentityResult> Add(string name);
    }

    public class ApplicationRoleRepository : IApplicationRoleRepository
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public ApplicationRoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public Task<List<ApplicationRole>> GetAll() => roleManager.Roles.ToListAsync();

        public Task<IdentityResult> Add(string name)
        {
            return roleManager.UpdateAsync(new ApplicationRole { Name = name });
        }
    }
}

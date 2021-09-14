using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VacationManagerApi.Repositories
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetAll();
        Task<IdentityResult> Add(string name);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public Task<List<IdentityRole>> GetAll() => roleManager.Roles.ToListAsync();

        public Task<IdentityResult> Add(string name) => roleManager.UpdateAsync(new(name));
    }
}

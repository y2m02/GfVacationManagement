using System.Collections.Generic;
using System.Threading.Tasks;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<IdentityResult> Add(ApplicationUser user, string password);
        Task<List<string>> GetRoles(string userName);
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<List<string>> GetRoles(string userName)
        {
            var user = await userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleAsync(u => u.UserName == userName)
                .ConfigureAwait(false);

            return user.UserRoles.EagerSelect(x => x.Role.Name);
        }

        public Task<IdentityResult> Add(ApplicationUser user, string password)
        {
            return userManager.CreateAsync(user, password);
        }
    }
}

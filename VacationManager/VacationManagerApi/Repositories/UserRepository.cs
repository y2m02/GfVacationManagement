using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> Add(ApplicationUser user, string password);
        Task<List<string>> GetRoles(string userName);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // TODO: This is horrible and MUST be improved!
        public async Task<List<string>> GetRoles(string userName)
        {
            var userId = (await userManager.Users
                .SingleAsync(u => u.UserName == userName)
                .ConfigureAwait(false)).Id;

            var roles = await userManager
                .GetRolesAsync(new() { Id = userId })
                .ConfigureAwait(false);

            return roles.ToList();
        }

        public Task<IdentityResult> Add(ApplicationUser user, string password)
        {
            return userManager.CreateAsync(user, password);
        }
    }
}

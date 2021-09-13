using System.Threading.Tasks;
using AutoMapper;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Services
{
    public interface IApplicationUserService
    {
        Task<IBaseResponse> Register(RegisterUserRequest request);
    }

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserService(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
        )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IBaseResponse> Register(RegisterUserRequest request)
        {
            var user = mapper.Map<ApplicationUser>(request);

            var result = await userManager
                .CreateAsync(user, request.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return new Failure(result.Errors.Join("\n"));
            }

            await userManager.AddToRoleAsync(user, "Admin").ConfigureAwait(false);

            await signInManager
                .SignInAsync(user, isPersistent: false)
                .ConfigureAwait(false);

            return new Success(user.Id);
        }
    }
}

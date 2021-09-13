using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseApiController
    {
        private readonly IApplicationUserService service;

        public ApplicationUserController(IApplicationUserService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            return OkResponse(await service.Register(user).ConfigureAwait(false));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SingInRequest user)
        {
            return OkResponse(await service.SingIn(user).ConfigureAwait(false));
        }
    }
}

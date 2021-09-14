using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest user)
        {
            var response = await service.Register(user).ConfigureAwait(false);

            return response.HasValidations() ? BadRequest(response) : OkResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SingInRequest user)
        {
            var response = await service.SingIn(user).ConfigureAwait(false);

            return response.Unauthorized() ? Unauthorized(response) : OkResponse(response);
        }
    }
}

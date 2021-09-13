using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RegisterUserRequest user)
        {
            return OkResponse(await service.Register(user).ConfigureAwait(false));
        }
    }
}

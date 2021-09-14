using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Helpers;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {
        private readonly IApplicationRoleService service;

        public RoleController(IApplicationRoleService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            return OkResponse(await service.GetRoles().ConfigureAwait(false));
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Admin)]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            var response = await service.CreateRole(name).ConfigureAwait(false);

            return response.HasValidations() ? BadRequest(response) : OkResponse(response);
        }
    }
}

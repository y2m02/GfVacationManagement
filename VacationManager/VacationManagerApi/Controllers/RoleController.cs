using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {
        private readonly IApplicationUserRoleService service;

        public RoleController(IApplicationUserRoleService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return OkResponse(await service.GetRoles().ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            return OkResponse(await service.CreateRole(name).ConfigureAwait(false));
        }
    }
}

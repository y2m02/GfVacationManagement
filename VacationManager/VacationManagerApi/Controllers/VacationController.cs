using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Helpers;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    public class VacationController : BaseApiController
    {
        private readonly IVacationService service;

        public VacationController(IVacationService service) => this.service = service;

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.AdminOrCanRead)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50
        )
        {
            return OkResponse(await service.GetAll(pageNumber, pageSize).ConfigureAwait(false));
        }

        [HttpGet("{id:required}")]
        [Authorize(Roles = ApplicationRoles.AdminOrCanRead)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetById(id).ConfigureAwait(false);

            if (response.Failed())
            {
                return InternalServerError(response);
            }

            return response is Success { Response: null }
                ? NotFound(new { id })
                : Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.CanAdd)]
        public async Task<IActionResult> Create(VacationRequest request)
        {
            var response = await service.Create(request).ConfigureAwait(false);

            if (response is not Success success)
            {
                return InternalServerError(response);
            }

            var vacation = (VacationDto)success.Response;

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { controller = "Vacation", id = vacation.Id },
                value: vacation
            );
        }

        [HttpPut("{id:required}")]
        [Authorize(Roles = ApplicationRoles.AdminOrCanUpdate)]
        public async Task<IActionResult> Update(int id, VacationRequest request)
        {
            return OkResponse(await service.Update(id, request).ConfigureAwait(false));
        }

        [HttpDelete("{id:required}")]
        [Authorize(Roles = ApplicationRoles.AdminOrCanDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContentResponse(await service.Delete(id).ConfigureAwait(false));
        }
    }
}

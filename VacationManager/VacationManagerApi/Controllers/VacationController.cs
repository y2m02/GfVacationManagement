using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll()
        {
            return OkResponse(await service.GetAll().ConfigureAwait(false));
        }

        [HttpGet("{id:required}")]
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
        public async Task<IActionResult> Update(int id, VacationRequest request)
        {
            return OkResponse(await service.Update(id, request).ConfigureAwait(false));
        }

        [HttpDelete("{id:required}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContentResponse(await service.Delete(id).ConfigureAwait(false));
        }
    }
}

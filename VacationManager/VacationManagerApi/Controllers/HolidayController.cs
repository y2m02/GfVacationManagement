using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    public class HolidayController : BaseApiController
    {
        private readonly IHolidayService service;

        public HolidayController(IHolidayService service) => this.service = service;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50
        )
        {
            return OkResponse(await service.GetAll(pageNumber, pageSize).ConfigureAwait(false));
        }

        [HttpGet("{id:required}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(HolidayRequest request)
        {
            var response = await service.Create(request).ConfigureAwait(false);

            if (response is not Success success)
            {
                return InternalServerError(response);
            }

            var holiday = (HolidayDto)success.Response;

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { controller = "Holiday", id = holiday.Id },
                value: holiday
            );
        }

        [HttpPut("{id:required}")]
        public async Task<IActionResult> Update(int id, HolidayRequest request)
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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    public class HolidayController : BaseApiController
    {
        private readonly IHolidayService service;

        public HolidayController(IHolidayService service) => this.service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ValidateResponse(await service.GetAll().ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ValidateResponse(await service.GetById(id).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Create(HolidayRequest request)
        {
            return ValidateResponse(await service.Create(request).ConfigureAwait(false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HolidayRequest request)
        {
            return ValidateResponse(await service.Update(id, request).ConfigureAwait(false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ValidateResponse(await service.Delete(id).ConfigureAwait(false));
        }
    }
}

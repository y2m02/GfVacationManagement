using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Services;

namespace VacationManagerApi.Controllers
{
    public class VacationController : BaseApiController
    {
        private readonly IVacationService service;

        public VacationController(IVacationService service) => this.service = service;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return ValidateResponse(await service.GetAll().ConfigureAwait(false));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ValidateResponse(await service.GetById(id).ConfigureAwait(false));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateVacationRequest request)
        {
            return ValidateResponse(await service.Create(request).ConfigureAwait(false));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateVacationRequest request)
        {
            return ValidateResponse(await service.Update(request).ConfigureAwait(false));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ValidateResponse(await service.Delete(id).ConfigureAwait(false));
        }
    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult ValidateResponse(BaseResponse response)
        {
            if (response.Succeeded())
            {
                return Ok(response);
            }

            return response.HasValidations()
                ? BadRequest(response)
                : InternalServerError(response);
        }
    }
}

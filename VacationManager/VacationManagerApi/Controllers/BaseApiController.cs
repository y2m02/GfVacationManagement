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

        protected IActionResult ValidateResult(BaseResponse baseResponse)
        {
            if (baseResponse.Succeeded())
            {
                return Ok(baseResponse);
            }

            return baseResponse.HasValidations()
                ? BadRequest(baseResponse)
                : InternalServerError(baseResponse);
        }
    }
}

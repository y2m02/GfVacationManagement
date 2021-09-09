using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ObjectResult Created(object value)
        {
            return StatusCode((int)HttpStatusCode.Created, value);
        }

        protected ObjectResult NoContent(object value)
        {
            return StatusCode((int)HttpStatusCode.NoContent, value);
        }

        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult ValidateResponse(
            Func<IBaseResponse, ObjectResult> success,
            IBaseResponse response
        )
        {
            if (response.HasValidations())
            {
                return BadRequest(response);
            }

            return response.Succeeded() ? success(response) : InternalServerError(response);
        }
    }
}

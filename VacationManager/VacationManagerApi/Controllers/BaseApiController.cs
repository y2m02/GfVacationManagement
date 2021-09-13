using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult OkResponse(IBaseResponse response)
        {
            return ValidateResponse(Ok, response);
        }

        protected IActionResult NoContentResponse(IBaseResponse response)
        {
            return ValidateResponse(NoContent, response);
        }

        private IActionResult ValidateResponse(
            Func<IBaseResponse, ObjectResult> success,
            IBaseResponse response
        )
        {
            return response.Succeeded() ? success(response) : InternalServerError(response);
        }

        private ObjectResult NoContent(object value)
        {
            return StatusCode((int)HttpStatusCode.NoContent, value);
        }
    }
}

﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationManager.Models.Responses;

namespace VacationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult ValidateResult(Result result)
        {
            if (result.Succeeded())
            {
                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }
    }
}

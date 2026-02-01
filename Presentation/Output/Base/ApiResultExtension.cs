using Common.Output;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Output.Base
{
    public static class ApiResultExtension
    {
        public static IActionResult ApiResult(this ControllerBase controller
            , ResultDto<object> resultDto)
        {
            if (!resultDto.IsSuccess)
                resultDto.Data = null;

            return resultDto.StatusCode switch
            {
                HttpStatusCode.OK => controller.Ok(resultDto),
                HttpStatusCode.Created => controller.StatusCode(201, resultDto),
                HttpStatusCode.Accepted => controller.Accepted(resultDto),

                HttpStatusCode.BadRequest => controller.BadRequest(resultDto),
                HttpStatusCode.NotFound => controller.NotFound(resultDto),
                HttpStatusCode.Forbidden => controller.StatusCode(403, resultDto),
                HttpStatusCode.Conflict => controller.Conflict(resultDto),
                HttpStatusCode.InternalServerError => controller.StatusCode(500, resultDto),

                _ => controller.StatusCode((int)resultDto.StatusCode, resultDto),
            };
        }
    }
}

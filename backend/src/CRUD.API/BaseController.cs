using CRUD.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRUD.API
{
    public abstract class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        [NonAction]
        public IActionResult ArgumentExceptionHandling(Exception ex)
        {
            var response = new ExceptionHandlingResponse();
            response.Error = ex.Message;
            return BadRequest(response);
        }

        [NonAction]
        public IActionResult ExceptionHandling(Exception ex)
        {
            var response = new ExceptionHandlingResponse();
            response.Error = ex.Message;
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [NonAction]
        public IActionResult AuthenticationExceptionHandling(Exception ex)
        {
            var response = new ExceptionHandlingResponse();
            response.Error = string.Empty;
            return StatusCode((int)HttpStatusCode.Unauthorized, response);
        }
        
        [NonAction]
        public IActionResult NotFoundExceptionHandling(Exception ex)
        {
            var response = new ExceptionHandlingResponse();
            response.Error = ex.Message;
            return NotFound(response);
        }
    }

    public class ExceptionHandlingResponse : BaseResponse
    {
        public ExceptionHandlingResponse()
        {
            this.Success = false;
        }
    }
}

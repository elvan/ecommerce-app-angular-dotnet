using System;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised()
        {
            return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }

        [HttpGet("invalid-reference/{id}")]
        public ActionResult GetInvalidReference(int id)
        {
            return Ok();
        }
    }
}

using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            Product thing = _context.Products.Find(42);

            return thing == null ? NotFound(new ApiResponse(404)) : Ok();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            Product thing = _context.Products.Find(42);

            string thingToReturn = thing.ToString();

            return Ok(thingToReturn);
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
            return Ok();
        }
    }
}

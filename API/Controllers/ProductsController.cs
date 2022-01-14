using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetProducts()
        {
            return Ok("This will be a list of products");
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            return Ok("This will be a single product with id of " + id);
        }
    }
}

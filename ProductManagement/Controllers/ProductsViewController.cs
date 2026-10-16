using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers
{
    [Route("products")]
    [Route("[controller]")]
    public class ProductsViewController : Controller
    {
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

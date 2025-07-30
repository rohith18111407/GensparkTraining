using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MigrationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public IActionResult Get()
            => Ok("Welcome to the ShopOnline API!");
    }
}

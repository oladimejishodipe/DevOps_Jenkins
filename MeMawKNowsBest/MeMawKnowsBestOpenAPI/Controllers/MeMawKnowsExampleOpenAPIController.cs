using Microsoft.AspNetCore.Mvc;

namespace MeMawKnowsBestOpenAPI.Controllers
{
    public class MeMawKnowsExampleOpenAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

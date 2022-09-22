using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class FormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

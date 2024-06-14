using Microsoft.AspNetCore.Mvc;

namespace Swaed.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

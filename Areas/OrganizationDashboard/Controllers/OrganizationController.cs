using Microsoft.AspNetCore.Mvc;

namespace Swaed.Areas.OrganizationDashboard.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

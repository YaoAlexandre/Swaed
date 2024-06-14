using Microsoft.AspNetCore.Mvc;

namespace Swaed.Controllers
{
    public class VolunteerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

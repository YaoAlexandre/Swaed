using Microsoft.AspNetCore.Mvc;
using Swaed.Models;

namespace Swaed.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();

        }


        // ADD NEW EVENTS
        [HttpPost]
        public async Task<IActionResult> AddEvent(Event model)
        {
            if (!ModelState.IsValid)
            {

            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindEvent(string id)
        {
            return View("Index");
        }
    }

}

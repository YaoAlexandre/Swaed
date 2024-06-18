using Microsoft.AspNetCore.Mvc;
using Swaed.Models;

namespace Swaed.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Training model)
        {
            if (!ModelState.IsValid)
            {

            }
            return View("Index");
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;

namespace site.Controllers
{
    public class ExperienceController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
        // GET
        [HttpPost]
        public void Keki(ExperienceController model)
        {
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("anime");
            Console.WriteLine("\n\n\n\n");
        }
    }
}
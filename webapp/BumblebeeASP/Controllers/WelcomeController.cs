using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Models;
using Microsoft.AspNetCore.Mvc;
using BumblebeeASP.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BumblebeeASP.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Route("Welcome")]
        public ActionResult FirstRun()
        {
            if (TempData["personModel"] != null)
            {
                PersonModel person = TempData.Get<PersonModel>("personModel");
                ViewBag.FirstName = person.FirstName;
            }
            return View("~/Views/Onboarding/Welcome.cshtml");
        }
    }
}

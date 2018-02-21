using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BumblebeeASP.Models;
using BumblebeeASP.Helpers;

namespace BumblebeeASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewBag.SuccessMessage = TempData["message"].ToString();
                TempData.Remove("message");
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Register()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //try and get a token
                string TokenAttempt = APIHelper.CheckToken(loginModel);
                if (TokenAttempt == "error")
                {
                    ViewBag.errorMessage = "There was a problem with your login attempt";
                }
                else
                {
                    PersonModel Person = APIHelper.GetPersonFromLogin(loginModel);
                    //check if person needs to go to onboarding or dashboard
                    if (Person.FinishedOnboarding == false)
                    {
                        return RedirectToAction("Index", controllerName: "Welcome");
                    }
                    else
                    {
                        return RedirectToAction("Index", controllerName: "MainContent");
                    }
                }
            }
            return View("Index", loginModel);
        }
    }
}

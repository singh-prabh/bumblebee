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

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel) {
            //model is valid
            if (ModelState.IsValid) 
            {
                //get a valid token for api
                string WebToken = APIHelper.CheckToken(loginModel);
                //issue generating token
                if (WebToken == "error") 
                {
                    ViewBag.errorMessage = "There was a problem with your login attempt";
                }
                //token is good, get person model
                else
                {
                    PersonModel Person = APIHelper.GetPersonFromLogin(loginModel);
                    //check if user should go to onboarding or dashboard
                    if (Person.FinishedOnboarding == false)
                    {
                        //go to onboarding
                        return RedirectToAction("Index", controllerName: "Welcome");
                    }
                    else
                    {
                        //go to dashboard
                        return RedirectToAction("Index", controllerName: "Dashboard");
                    }
                }
            }
            //model isn't valid
            return View("Index", loginModel);
        }
    }
}

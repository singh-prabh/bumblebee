using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Helpers;
using BumblebeeASP.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BumblebeeASP.Controllers
{
    public class RegisterController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult SaveUser(RegisterModel registerModel)
        {
            PrinterClass.printDebugMessage("create button pressed on register page");
            if (ModelState.IsValid)
            {
                //try to register new user
                Boolean DidRegister = APIHelper.RegisterNewUser(registerModel);
                if (DidRegister == true)
                {
                    //send user back to Login page
                    TempData["message"] = "User created successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //error registering new user
                    ViewBag.ErrorMessage = "Something went wrong with your registration";
                }
            }
            return View("~/Views/Home/Register.cshtml",registerModel);
        }
    }
}

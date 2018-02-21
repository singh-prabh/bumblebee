using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using BumblebeeASP.Models;
using BumblebeeASP.Helpers;

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
            if (ModelState.IsValid)
            {
                //try to register user 
                Boolean DidRegister = APIHelper.RegisterNewUser(registerModel);
                if (DidRegister == true)
                {
                    //send user back to login page
                    TempData["message"] = "User created successfully";
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    //error with registration
                    ViewBag.ErrorMessage = "Something went wrong with your registration";
                }
            }
            return View("~/Views/Home/Register.cshtml", registerModel);
        }
    }
}

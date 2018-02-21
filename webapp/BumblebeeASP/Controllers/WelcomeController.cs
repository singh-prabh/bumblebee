using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Helpers;
using BumblebeeASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BumblebeeASP.Controllers
{
    public class WelcomeController : Controller
    {

        private void ViewBagUpdate()
        {
            //use person model to populate labels
            PersonModel Person = APIHelper.MainPerson;
            ViewBag.UserName = Person.FirstName + " " + Person.LastName;
            //setup select list for view
            ViewBag.StateList = Converters.CreateStateSelectList();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBagUpdate();
            return View("~/Views/Onboarding/Welcome.cshtml");
        }

        [Route("Welcome")]
        [HttpPost]
        public ActionResult SaveData(CompanyModel companyModel)
        {
            //check if model is filled out
            if (ModelState.IsValid)
            {
                bool dataSaved = APIHelper.SaveCompanyForUser(companyModel);
                if (dataSaved == true)
                {
                    return RedirectToAction("Index", controllerName: "Dashboard");
                }
            }
            ViewBagUpdate();
            //return view
            return View("~/Views/Onboarding/Welcome.cshtml", companyModel);
        }
    }
}

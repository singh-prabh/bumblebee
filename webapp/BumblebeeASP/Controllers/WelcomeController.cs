using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Models;
using Microsoft.AspNetCore.Mvc;
using BumblebeeASP.Helpers;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BumblebeeASP.Controllers
{
    public class WelcomeController : Controller
    {
        private List<StatesModel> list;
        List<SelectListItem> StatesList { 
            get
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                if (list == null)
                {
                    list = APIHelper.GetStateList();
                    foreach (StatesModel state in list)
                    {
                        listItems.Add(new SelectListItem()
                        {
                            Text = state.StateAbbreviation + " - " + state.StateName,
                            Value = state.StateID.ToString()
                        });
                    }
                }
                return listItems;
            }
        }

        private void ViewBagUpdate()
        {
            //use person model to populate labels
            PersonModel Person = APIHelper.MainPerson;
            ViewBag.FirstName = Person.FirstName;
            //setup select list for view
            ViewBag.StateList = new SelectList(StatesList, "Value", "Text");
        }

        [Route("Welcome")]
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
                bool dataSaved = APIHelper.SaveCompanyForPerson(companyModel);
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

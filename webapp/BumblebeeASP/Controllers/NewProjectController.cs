using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using BumblebeeASP.Models;
using BumblebeeASP.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BumblebeeASP.Controllers
{
    public class NewProjectController : Controller
    {
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult SaveProject(ProjectModel projectModel, int? ExistingCompany)
        {
            if (projectModel.CompanyOption == "customerOption1")
            {
                //user chose an existing customer so just create the project
                int CompanyID = (int)ExistingCompany;
                //setup end date for project based on entered timeframe
                int TimeFrame = projectModel.EnteredTimeAmount;
                int TimeOption = projectModel.EnteredTimeOption;

            }
            ViewBagHelper.SetupNewProjectViewBag(this);
            return View("~/Views/Main/MainContent.cshtml",projectModel);
        }

        public ActionResult CancelProject()
        {
            return View("~/Views/Main/MainContent.cshtml");
        }
    }
}

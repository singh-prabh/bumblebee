using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using BumblebeeASP.Models;

namespace BumblebeeASP.Controllers
{
    public class MainContentController : Controller
    {
        private void GetProjectList()
        {
            var list = APIHelper.GetProjectList();
            if ((list != null) && (list.Count() > 0))
            {
                PrinterClass.printDebugMessage("the project list has stuff in it");
            }
            else
            {
                PrinterClass.printErrorMessage("the project list has nothing in it");
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            GetProjectList();
            return View("~/Views/Main/MainContent.cshtml");
        }

        public ActionResult FilterAll()
        {
            return View("~/Views/Main/MainContent.cshtml");
        }

        public ActionResult FilterOpen()
        {
            return View("~/Views/Main/MainContent.cshtml");
        }

        public ActionResult FilterClosed()
        {
            return View("~/Views/Main/MainContent.cshtml");
        }

        public ActionResult AddProject()
        {
            ViewBagHelper.SetupNewProjectViewBag(this);
            return View("~/Views/Main/MainContent.cshtml");
        }

        [HttpPost]
        public ActionResult SaveProject(ProjectModel projectModel)
        {
            return View("~/Views/Main/MainContent.cshtml", projectModel);
        }


        public ActionResult ProjectForm()
        {
            return PartialView("NewProject");
        }
    }
}

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

        public static int ProjectSelectedIndex = 0;
        public static int NavigationSelectedIndex = -1;

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBagHelper.SetupSideBarProjects(this, ProjectSelectedIndex);
            ViewBagHelper.SetupNavigationBar(this, NavigationSelectedIndex);
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

        public ActionResult SelectedNavLink(int index)
        {
            NavigationSelectedIndex = index;
            ViewBagHelper.SetupSideBarProjects(this, ProjectSelectedIndex);
            ViewBagHelper.SetupNavigationBar(this, NavigationSelectedIndex);
            return View("/Views/Main/MainContent.cshtml");
        }
    }
}

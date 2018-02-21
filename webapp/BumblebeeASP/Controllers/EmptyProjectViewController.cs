using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BumblebeeASP.Controllers
{
    public class EmptyProjectViewController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddProject()
        {
            ViewBagHelper.SetupNewProjectViewBag(this);
            return View("~/Views/Main/MainContent.cshtml");
        }
    }
}

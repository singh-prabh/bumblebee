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

        void SetupProjectEndTime(ProjectModel projectModel)
        {
            //setup end date for project based on entered timeframe
            int TimeFrame = projectModel.EnteredTimeAmount;
            String TimeOption = projectModel.EnteredTimeOption;
            switch (TimeOption)
            {
                case "hours":
                    projectModel.EndDate = projectModel.StartDate.AddHours(TimeFrame);
                    break;
                case "days":
                    projectModel.EndDate = projectModel.StartDate.AddDays(TimeFrame);
                    break;
                case "weeks":
                    projectModel.EndDate = projectModel.StartDate.AddDays((TimeFrame * 7));
                    break;
                case "months":
                    projectModel.EndDate = projectModel.StartDate.AddMonths(TimeFrame);
                    break;
            }
        }

        public ActionResult SaveProject(ProjectModel projectModel, int? ExistingCompany)
        {
            if (projectModel.CompanyOption == "customerOption1")
            {
                //user chose an existing customer so just create the project
                CompanyModel ExistingCompanyModal = new CompanyModel
                {
                    CompanyID = (int)ExistingCompany
                };
                SetupProjectEndTime(projectModel);
                //try to save the project
                var projectSaved = APIHelper.SaveProjectForCompany(projectModel, ExistingCompanyModal);
                if (projectSaved == true)
                {
                    ViewBagHelper.ProjectCreated(this);
                    return View("~/Views/Main/MainContent.cshtml");
                }
                else
                {
                    ViewBagHelper.ErrorSavingProjectViewBag(this);
                    return View("~/Views/Main/MainContent.cshtml", projectModel);
                }

            }
            else if (projectModel.CompanyOption == "customerOption2")
            {
                if (ModelState.IsValid)
                {
                    //user chose a new company.  
                    SetupProjectEndTime(projectModel);
                    var projectSaved = APIHelper.SaveProjectAndCompany(projectModel);
                    if (projectSaved == true)
                    {
                        ViewBagHelper.ProjectCreated(this);
                        return View("~/Views/Main/MainContent.cshtml");
                    }
                    else
                    {
                        ViewBagHelper.ErrorSavingProjectViewBag(this);
                        return View("~/Views/Main/MainContent.cshtml", projectModel);
                    }
                }
            }
            //something went wrong to get this far
            ViewBagHelper.SetupNewProjectViewBag(this);
            return View("~/Views/Main/MainContent.cshtml",projectModel);
        }


        public ActionResult CancelProject()
        {
            ViewBagHelper.SetupNavigationBar(this, MainContentController.NavigationSelectedIndex);
            ViewBagHelper.SetupSideBarProjects(this, MainContentController.ProjectSelectedIndex);
            return View("~/Views/Main/MainContent.cshtml");
        }
    }
}

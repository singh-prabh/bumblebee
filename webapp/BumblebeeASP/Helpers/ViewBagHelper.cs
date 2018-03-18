using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BumblebeeASP.Controllers;
using BumblebeeASP.Models;
namespace BumblebeeASP.Helpers
{
    public static class ViewBagHelper
    {
        
        public static void SetupNavigationBar(Controller controller, int selectedIndex)
        {
            //create the navigation list
            var NavLinks = new List<NavigationModel>
            {
                new NavigationModel()
                {
                    Name = "Dashboard",
                    IconString = "fas fa-home fa-fw"
                },
                new NavigationModel()
                {
                    Name = "Tasks",
                    IconString = "fas fa-tasks fa-fw"
                },
                new NavigationModel()
                {
                    Name = "Invoices",
                    IconString = "fas fa-file-alt fa-fw"
                },
                new NavigationModel()
                {
                    Name = "Settings",
                    IconString = "fas fa-cog fa-fw"
                }
            };
            //add the list and index to the ViewBag
            controller.ViewBag.NavLinks = NavLinks;
            controller.ViewBag.NavCount = NavLinks.Count();
            controller.ViewBag.NavSelectedIndex = selectedIndex;
            SetupViewName(controller,selectedIndex);
        }

        static void SetupViewName(Controller controller, int index)
        {
            var name = "";
            switch (index)
            {
                case 0:
                    name = "Dashboard";
                    break;
                case 1:
                    name = "Tasks";
                    break;
                case 2:
                    name = "Invoices";
                    break;
                case 3:
                    name = "Settings";
                    break;
            }
            controller.ViewBag.ViewName = name;
        }

        public static void SetupSideBarProjects(Controller controller, int selectedIndex)
        {
            //get project list (if any)
            var Projects = APIHelper.GetProjectList();
            if (Projects.Any())
            {
                if (MainContentController.NavigationSelectedIndex == -1)
                {
                    MainContentController.NavigationSelectedIndex = 0; 
                }
                controller.ViewBag.ProjectCount = Projects.Count();
                controller.ViewBag.ProjectList = Projects;
                controller.ViewBag.ProjectSelectedIndex = selectedIndex;
            }
        }

        public static void SetupNewProjectViewBag(Controller controller)
        {
            controller.ViewBag.AddProject = "true";
            controller.ViewBag.CompanyList = Converters.CreateCompanySelectList();
            controller.ViewBag.StateList = Converters.CreateStateSelectList();
            controller.ViewBag.TimeFrames = Converters.CreateProjectTimeFrameSelectList();
        }

        public static void ErrorSavingProjectViewBag(Controller controller)
        {
            controller.ViewBag.ProjectError = "Something went wrong while saving the project";
            SetupNewProjectViewBag(controller);
        }

        public static void ProjectCreated(Controller controller)
        {
            int navIndex = MainContentController.NavigationSelectedIndex < 0 ? 0 : MainContentController.NavigationSelectedIndex;
            MainContentController.NavigationSelectedIndex = navIndex;
            SetupNavigationBar(controller, navIndex);
            SetupSideBarProjects(controller, MainContentController.ProjectSelectedIndex);
        }
    }
}

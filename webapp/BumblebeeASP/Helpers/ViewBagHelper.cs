using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BumblebeeASP.Helpers
{
    public static class ViewBagHelper
    {
        public static void SetupNewProjectViewBag(Controller controller)
        {
            controller.ViewBag.AddProject = "true";
            controller.ViewBag.CompanyList = Converters.CreateCompanySelectList();
            controller.ViewBag.StateList = Converters.CreateStateSelectList();
            controller.ViewBag.TimeFrames = Converters.CreateProjectTimeFrameSelectList();
        }
    }
}

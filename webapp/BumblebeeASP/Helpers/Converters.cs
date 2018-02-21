using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using BumblebeeASP.Models;

namespace BumblebeeASP.Helpers
{
    public static class Converters
    {
        public static SelectList CreateCompanySelectList()
        {
            List<SelectListItem> SelectList = new List<SelectListItem>();
            var CompanyList = APIHelper.GetCompanyList();
            if (CompanyList.Any())
            {
                foreach (CompanyModel company in CompanyList)
                {
                    SelectList.Add(new SelectListItem()
                    {
                        Text = company.CompanyName,
                        Value = company.CompanyID.ToString()
                    });
                }
            }
            return new SelectList(SelectList, "Value", "Text");
        }

        public static SelectList CreateStateSelectList()
        {
            List<SelectListItem> SelectList = new List<SelectListItem>();
            var StateList = APIHelper.GetStateList();
            if (StateList.Any())
            {
                foreach (StatesModel state in StateList)
                {
                    SelectList.Add(new SelectListItem()
                    {
                        Text = state.StateAbbreviation + " - " + state.StateName,
                        Value = state.StateID.ToString()
                    });
                }
            }
            return new SelectList(SelectList, "Value", "Text");
        }

        public static SelectList CreateProjectTimeFrameSelectList()
        {
            List<SelectListItem> SelectList = new List<SelectListItem>();
            SelectList.Add(new SelectListItem()
            {
                Text = "hours",
                Value = "hours"
            });
            SelectList.Add(new SelectListItem()
            {
                Text = "days",
                Value = "days"
            });
            SelectList.Add(new SelectListItem()
            {
                Text = "weeks",
                Value = "weeks"
            });
            SelectList.Add(new SelectListItem()
            {
                Text = "months",
                Value = "months"
            });
            return new SelectList(SelectList, "Value", "Text");
        }
    }
}

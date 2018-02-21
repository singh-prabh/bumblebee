using System;
using System.ComponentModel.DataAnnotations;
using RestSharp.Deserializers;
namespace BumblebeeASP.Models
{
    public class ProjectModel
    {
        [DeserializeAs(Name = "id")]
        public int ProjectID { get; set; }

        [DeserializeAs(Name = "name")]
        [Display(Prompt = "enter a project name", Name = "Project Name")]
        [Required(ErrorMessage = "A project name is required")]
        public string ProjectName { get; set; }

        [DeserializeAs(Name = "notes")]
        [Display(Prompt = "a short description of the project", Name = "Project Notes")]
        public string ProjectNotes { get; set; }

        [DeserializeAs(Name = "startDate")]
        [Display(Prompt = "start date", Name = "Start Date")]
        [Required(ErrorMessage = "A start date is required")]
        public DateTime StartDate { get; set; }

        [DeserializeAs(Name = "endDate")]
        public DateTime EndDate { get; set; }

        [DeserializeAs(Name = "statusID")]
        public int ProjectStatus { get; set; }

        [DeserializeAs(Name = "companyID")]
        public int CompanyID { get; set; }

        [DeserializeAs(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DeserializeAs(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public int EnteredTimeAmount { get; set; }

        public int EnteredTimeOption { get; set; }

        [Display(Name = "Customer")]
        public string CompanyOption { get; set; }

        public CompanyModel ProjectCompany { get; set; }
    }
}

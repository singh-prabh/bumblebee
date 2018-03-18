using System;
using RestSharp.Deserializers;
using System.ComponentModel.DataAnnotations;

namespace BumblebeeASP.Models
{
    
    public class PersonModel
    {
        [DeserializeAs(Name = "id")]
        public int PersonID { get; set; }

        [DeserializeAs(Name = "firstName")]
        [Display(Prompt = "first name")]
        public string FirstName { get; set; }

        [DeserializeAs(Name = "lastName")]
        [Display(Prompt = "last name")]
        public string LastName { get; set; }

        [DeserializeAs(Name = "companyID")]
        public int CompanyID { get; set; }

        [DeserializeAs(Name = "email")]
        [Display(Prompt = "name@address.com")]
        [EmailAddress(ErrorMessage = "Email format is incorrect")]
        public string PersonEmail { get; set; }

        [DeserializeAs(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DeserializeAs(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [DeserializeAs(Name = "finishedOnboarding")]
        public bool FinishedOnboarding { get; set; }
    }
}

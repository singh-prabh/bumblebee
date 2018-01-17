using System;
using RestSharp.Deserializers;

namespace BumblebeeASP.Models
{
    
    public class PersonModel
    {
        [DeserializeAs(Name = "id")]
        public int PersonID { get; set; }
        [DeserializeAs(Name = "firstName")]
        public string FirstName { get; set; }
        [DeserializeAs(Name = "lastName")]
        public string LastName { get; set; }
        [DeserializeAs(Name = "companyID")]
        public int CompanyID { get; set; }
        [DeserializeAs(Name = "email")]
        public string PersonEmail { get; set; }
        [DeserializeAs(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [DeserializeAs(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }
        [DeserializeAs(Name = "finishedOnboarding")]
        public bool FinishedOnboarding { get; set; }
    }
}

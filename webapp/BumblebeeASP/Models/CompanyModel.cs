using System;
using System.ComponentModel.DataAnnotations;
using RestSharp.Deserializers;

namespace BumblebeeASP.Models
{
    public class CompanyModel
    {
        [DeserializeAs(Name = "id")]
        public int CompanyID { get; set; }

        [Display(Prompt = "company name", Name = "Company Name")]
        [Required(ErrorMessage = "A Company Name is Required")]
        [DeserializeAs(Name = "name")]
        public string CompanyName { get; set; }

        [Display(Prompt = "www.yourcompany.com", Name = "Company Website")]
        [DeserializeAs(Name = "website")]
        public string CompanyURL { get; set; }

        [Display(Prompt = "123-456-7890", Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression(@"^[0-9-]*\d{3}-\d{3}-\d{4}$", ErrorMessage = "Use xxx-xxx-xxxx format")]
        public string CompanyPhone { get; set; }

        [Display(Prompt = "street address 1")]
        [Required(ErrorMessage = "Street Address is Required")]
        public string CompanyStreet1 { get; set; }

        [Display(Prompt = "street address 2")]
        public string CompanyStreet2 { get; set; }

        [Display(Prompt = "city")]
        [Required(ErrorMessage = "City is Required")]
        public string CompanyCity { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "{0} is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} is Required")]
        public int? CompanyState { get; set; }

        [Display(Prompt = "zip")]
        [Required(ErrorMessage = "Zip is Required")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "just use 5 numbers")]
        [RegularExpression(@"^[0-9]{5,5}$", ErrorMessage = "just use 5 numbers")]
        public string CompanyZip { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace BumblebeeASP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Prompt = "email address")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Prompt = "password")]
        public string LoginPassword { get; set; }


        public string ErrorMessage { get; set; }
    }
}

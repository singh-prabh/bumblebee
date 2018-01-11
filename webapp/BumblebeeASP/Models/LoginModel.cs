using System;
using System.ComponentModel.DataAnnotations;

namespace BumblebeeASP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [Display(Prompt = "email address")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Prompt = "password")]
        public string password { get; set; }
    }
}

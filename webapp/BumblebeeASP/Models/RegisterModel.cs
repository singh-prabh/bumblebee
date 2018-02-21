using System;
using System.ComponentModel.DataAnnotations;

namespace BumblebeeASP.Models
{
    public class RegisterModel
    {
        [Display(Prompt = "first name", Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Display(Prompt = "last name", Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Display(Prompt = "you@example.com", Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is incorrect")]
        public string UserEmail { get; set; }
        [Display(Prompt = "create a password", Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage="Must be over 8 characters", MinimumLength = 8)]
        public string UserPassword { get; set; }
        public string ErrorMessage { get; set; }
    }
}

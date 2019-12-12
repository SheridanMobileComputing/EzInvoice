using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EzInvoice.Models
{
    public class SignupAttempt
    {
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; } = "";
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = "";
        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; } = "";
    }
}

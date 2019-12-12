using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

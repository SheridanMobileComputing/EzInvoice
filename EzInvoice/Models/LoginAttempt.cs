using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class LoginAttempt
    {
        public string Email_address { get; set; } = "";
        public string Password { get; set; } = "";

        public bool emptyAttempt()
        {
            return (Email_address == "") && (Password == "");
        }
        public bool wasSuccessful()
        {
            if( Email_address == "bill@gmail.com" &&
                Password      == "1234")
            {
                return true;
            }
            return false;
        }
    }
}

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
            //var user = _context.Users.FirstOrDefault(a => a.EmailAddress == Email);

            User UserAccount = Repository.getUserByEmail(Email_address);



            if (UserAccount == null)
            {
                return false;
            }

            if(UserAccount.Password == Password)
            {
                return true;
            }

            return false;
        }
    }
}

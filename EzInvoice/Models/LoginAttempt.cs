using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class LoginAttempt
    {
        public string EmailAddress { get; set; } = "";
        public string Password { get; set; } = "";

        public bool emptyAttempt()
        {
            return (EmailAddress == "") && (Password == "");
        }
        public bool wasSuccessful()
        {
            User UserAccount = Repository.getUserByEmail(EmailAddress);
            if(UserAccount == null)
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

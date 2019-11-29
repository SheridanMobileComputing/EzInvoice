using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class SignupAttempt
    {

        public string EmailAddress { get; set; } = "";
        public string Password { get; set; } = "";

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string RepeatPassword { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        /*public SignupAttempt(String First_Name, String Last_Name, String Email_Address, String Password, String Repeat_Password)
        {
            this.First_Name = First_Name;
            this.Last_Name = Last_Name;
            this.Email_Address = Email_Address;
            this.Password = Password;
            this.Repeat_Password = Repeat_Password;
        }*/

        public string getError()
        {
            if(this.FirstName == null || this.FirstName == "")
            {
                return "First Name cannot be empty.";
            }
            if (this.LastName == null || this.LastName == "")
            {
                return "Last Name cannot be empty.";
            }
            if (this.EmailAddress == null || this.EmailAddress == "")
            {
                return "Email Address cannot be empty.";
            }
            if (this.Password == null || this.Password == "")
            {
                return "Password cannot be empty.";
            }
            if (this.RepeatPassword == null || this.RepeatPassword == "")
            {
                return "Repeat Password cannot be empty.";
            }

            if (this.Password.Length < 6)
            {
                return "Password too short. Must be at least 6 characters.";
            }
            if(this.Password != this.RepeatPassword)
            {
                return "Passwords do not match!";
            }
            return "";
        }
    }
}

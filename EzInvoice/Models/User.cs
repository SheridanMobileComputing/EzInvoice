using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class User
    {
        private string First_name { get; set; }
        private string Last_name { get; set; }
        public string Email_address { get; set; }
        private string Password { get; set; }
        private string Street { get; set; }
        private string City { get; set; }
        private string Province { get; set; }
        private string Postal_Code { get; set; }
        private string Country { get; set; }

        public static User getUser(string name)
        {
            return new User
            {
                First_name = "Jam",
                Last_name = "Smith",
            };
        }

    }

}

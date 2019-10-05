using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class User
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email_address { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal_Code { get; set; }
        public string Country { get; set; }

    }
}

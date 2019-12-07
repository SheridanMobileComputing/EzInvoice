using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Client
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal_code { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public int CalcBalance()
        {

            /* Iterate through all invoices with this client
             * and return total value (positive or negative)
             * of this client's invoices*/
            return 0;
        }

        public bool GoodStanding()
        {
            /* Using the calcBalance method, if the 
             total balance is positive return true, else
             return false */
            return true;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Client
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal_code { get; set; }
        public string Country { get; set; }


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

        public static List<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    Name = "Jeremy Clark",
                    Street = "6969 Fake Street",
                    City = "Oakville",
                    Province = "Ontario",
                    Postal_code = "X9X9X9X",
                    Country = "Canada",
                },
                new Client()
                {
                    Name = "Apple Inc",
                    Street = "1 Apple Park Way",
                    City = "Cupertino",
                    Province = "California",
                    Postal_code = "95014",
                    Country = "United States",
                },
                new Client()
                {
                    Name = "Richard Sackler",
                    Street = "24 Oxycontin Rd",
                    City = "Austin",
                    Province = "Texas",
                    Postal_code = "78652",
                    Country = "United States",
                },
            };
        }

    }


}



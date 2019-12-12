using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Client
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Postal Code is required")]
        public string Postal_code { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public double CalcBalance(EZInvoiceDB context)
        {

            /* Iterate through all invoices with this client
             * and return total value (positive or negative)
             * of this client's invoices*/

            var invoices = context.Invoices
                .Include(i => i.Client)
                .Include(i => i.InvoiceItems)
                .Where(i => i.Client.Id == this.Id).ToList();

            double total = 0;
            foreach (var invoice in invoices)
            {
                total += invoice.Total();
            }
            return total;
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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Invoice
    {
        private int Id { get; set; }
        private DateTime Date_of_issue { get; set; }
        private DateTime Due_date { get; set; }
        private bool Paid { get; set; }
        private float Tax_rate { get; set; }


        private float TotalInvoiceAmt()
        {
            /* 
             
                Calculate the total invoice amount based off the individual
                invoice items with the same ID
             
             */
            return (float)0.0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date_of_issue { get; set; }
        public DateTime Due_date { get; set; }
        public bool Paid { get; set; }
        public float Tax_rate { get; set; }


        public float TotalInvoiceAmt()
        {
            /* 
             
                Calculate the total invoice amount based off the individual
                invoice items with the same ID
             
             */
            return (float)0.0;
        }
    }
}

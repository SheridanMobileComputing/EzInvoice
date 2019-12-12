using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EzInvoice.Models
{
    public class Invoice
    {
        public int? Id { get; set; }
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range")]
        public DateTime DateOfIssue { get; set; }
        [Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Date is out of Range")]
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public float TaxRate { get; set; }
        public Client Client { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }

        public double Total()
        {
            double cost = 0.00;
            if (InvoiceItems.Count > 0)
            {
                foreach (var item in InvoiceItems)
                {
                    cost += item.Total();
                }
            }
            return Math.Round(cost, 2);
        }

        public double Tax()
        {
            return Math.Round(Total() * 0.13, 2);
        }
    }
}

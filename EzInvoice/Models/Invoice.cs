using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Invoice
    {
        public int? Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public float TaxRate { get; set; }
        public Client Client { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }

        public double Total()
        {
            double cost = 0.00;

            foreach (var item in InvoiceItems)
            {
                cost += item.Total();
            }
            return Math.Round(cost, 2);
        }

        public double Tax()
        {
            return Math.Round(Total() * 0.13, 2);
        }
    }
}

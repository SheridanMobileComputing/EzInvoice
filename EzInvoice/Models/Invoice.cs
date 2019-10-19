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
        public string Client { get; set; }

        public List<InvoiceItem> Items { get; set; }

        private float TotalInvoiceAmt()
        {
            var cost = (float)0.0;

            foreach (var item in Items)
            {
                cost += item.Cost_each * item.Quantity;
            }
            return cost;
        }
    }
}

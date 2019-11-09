using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string ItemNo { get; set; }
        public string ItemDescription { get; set; }
        public double Cost { get; set; }
        public double Quantity { get; set; }

        public double Total()
        {
            return Math.Round(Cost * Quantity, 2);
        }
    }
}

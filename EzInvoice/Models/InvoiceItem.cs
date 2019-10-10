using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public float Cost_each { get; set; }

    }
}

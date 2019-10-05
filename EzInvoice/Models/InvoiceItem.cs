using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class InvoiceItem
    {
        private int Id { get; set; }
        private DateTime Date { get; set; }
        private float Quantity { get; set; }
        private string Description { get; set; }
        private float Cost_each { get; set; }

    }
}

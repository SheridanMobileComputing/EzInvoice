using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;

namespace EzInvoice.Models
{
    public static class Repository
    {

        private static List<Invoice> invoices = new List<Invoice>
        {
            new Invoice { Id = 0, Client = "Bob Barker", Date_of_issue = new DateTime(2015, 5, 1), Due_date = new DateTime(2018, 3, 11), Paid = false, Tax_rate = 0.13f },
            new Invoice { Id = 1, Client = "Chandler Bing", Date_of_issue = new DateTime(2017, 8, 14), Due_date = new DateTime(2018, 4, 15), Paid = true, Tax_rate = 0.13f },
            new Invoice { Id = 2, Client = "Adam Sandler", Date_of_issue = new DateTime(2018, 2, 25), Due_date = new DateTime(2018, 4, 30), Paid = false, Tax_rate = 0.13f },
        };

        private static List<InvoiceItem> ItemList = new List<InvoiceItem>
        {
            new InvoiceItem { Id = 0, Quantity = 5, Description = "Fidget Spinner", Cost_each = 12 },
            new InvoiceItem { Id = 1, Quantity = 200, Description = "Pre-Prohibition Four Loko (Cherry)", Cost_each = 4.99f },
            new InvoiceItem { Id = 2, Quantity = 25, Description = "AOL 1 month subscription CD", Cost_each = 4.99f }
        };

        public static IEnumerable<Invoice> InvoiceList
        { get { return invoices; } }
    }
}

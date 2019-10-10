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
        public int Client { get; set; }

        private List<InvoiceItem> Items { get; set; }

        private float TotalInvoiceAmt()
        {
            var cost = (float)0.0;

            foreach (var item in Items)
            {
                cost += item.Cost_each * item.Quantity;
            }
            return cost;
        }

        public static List<Invoice> GetInvoices()
        {
            return new List<Invoice>()
            {
                new Invoice() {
                    Id = 0,
                    Client = 1,
                    Date_of_issue = new DateTime(2015, 5, 1), //Year Month Day
                    Due_date = new DateTime(2018, 3, 11),

                    Paid = false,
                    Tax_rate = 0.13f,
                    Items = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            Id = 0,
                            Quantity = 5,
                            Description = "Fidget Spinner",
                            Cost_each = 12
                        },
                        new InvoiceItem()
                        {
                            Id = 1,
                            Quantity = 200,
                            Description = "Pre-Prohibition Four Loko (Cherry)",
                            Cost_each = 4.99f
                        },
                        new InvoiceItem()
                        {
                            Id = 2,
                            Quantity = 25,
                            Description = "AOL 1 month subscription CD",
                            Cost_each = 4.99f
                        }


                    }
                },
                new Invoice() {
                    Id = 1,
                    Client = 0,
                    Date_of_issue = new DateTime(2017, 5, 1), //Year Month Day
                    Due_date = new DateTime(2020, 09, 11),

                    Paid = true,
                    Tax_rate = 0.12f,
                    Items = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            Id = 0,
                            Quantity = 900,
                            Description = "The Joker Movie Ticket",
                            Cost_each = 15.99f
                        },
                        new InvoiceItem()
                        {
                            Id = 1,
                            Quantity = 99999,
                            Description = "Fake Eyelashes (Acrylic)",
                            Cost_each = 2.99f
                        },
                        new InvoiceItem()
                        {
                            Id = 2,
                            Quantity = 40,
                            Description = "Air Canada Flight to New Zealand (One Way)",
                            Cost_each = 949.99f
                        }
                    }
                },
                new Invoice() {
                    Id = 0,
                    Client = 1,
                    Date_of_issue = new DateTime(2001, 09, 11), //Year Month Day
                    Due_date = new DateTime(2021, 09, 11),

                    Paid = false,
                    Tax_rate = 0.13f,
                    Items = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            Id = 0,
                            Quantity = 0,
                            Description = "AKG Headphones (Stereo)",
                            Cost_each = 299.99f
                        },
                        new InvoiceItem()
                        {
                            Id = 1,
                            Quantity = 1,
                            Description = "Gibson Electric Guitar (Red)",
                            Cost_each = 199.99f
                        },
                        new InvoiceItem()
                        {
                            Id = 2,
                            Quantity = 69,
                            Description = "Arduino Uno Microcontroller",
                            Cost_each = 4.49f
                        }


                    }
                },
            };
            
        }
    }
}

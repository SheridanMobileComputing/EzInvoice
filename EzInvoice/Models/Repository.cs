using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;

namespace EzInvoice.Models
{
    public static class Repository
    {

        private static List<Client> clients = new List<Client>
        {
            new Client { Name = "Jeremy Clark",  Street = "6969 Fake Street", City = "Oakville", Province = "Ontario", Postal_code = "X9X9X9X", Country = "Canada", Email = "jclark@sheridancollege.ca"},
            new Client { Name = "Apple Inc", Street = "1 Apple Park Way", City = "Cupertino", Province = "California", Postal_code = "95014", Country = "United States",Email = "apple@support.ca"},
            new Client { Name = "Richard Sackler", Street = "24 Oxycontin Rd", City = "Austin", Province = "Texas", Postal_code = "78652", Country = "United States", Email = "dicksack@tx.com"},
        };

        // Example Invoice Items to test the Invoices
        private static List<InvoiceItem> ItemList = new List<InvoiceItem>
        {
            new InvoiceItem { ItemNo = "TOY-GEN-100", ItemDescription = "Fidget Spinner", Cost = 12.99, Quantity = 2 },
            new InvoiceItem { ItemNo = "TOY-BABY-300", ItemDescription = "Stacking Rings", Cost = 15.99, Quantity = 3 },
            new InvoiceItem { ItemNo = "TOY-BOY-260", ItemDescription = "Transformers - Megatron", Cost = 64.99, Quantity = 1 },
            new InvoiceItem { ItemNo = "TOY-GIRL-020", ItemDescription = "Alex DIY - Nail Kit", Cost = 24.99, Quantity = 4 },
            new InvoiceItem { ItemNo = "CARD-GEN-015", ItemDescription = "AOL 1 month sub", Cost = 4.99, Quantity = 4 },
            new InvoiceItem { ItemNo = "DRINK-18+-015", ItemDescription = "Pre-Prohibition Four Loko (Cherry)", Cost = 14.99, Quantity = 6 },
        };

        // Example Invoice Data to Test the system
        private static List<Invoice> invoices = new List<Invoice>
        {
            new Invoice { Id = 0, Client = clients[0], Date_of_issue = new DateTime(2015, 5, 1), Due_date = new DateTime(2018, 3, 11), Paid = false, Tax_rate = 0.13f, Items = ItemList },
            new Invoice { Id = 1, Client = clients[1], Date_of_issue = new DateTime(2017, 8, 14), Due_date = new DateTime(2018, 4, 15), Paid = true, Tax_rate = 0.13f, Items = ItemList },
            new Invoice { Id = 2, Client = clients[2], Date_of_issue = new DateTime(2018, 2, 25), Due_date = new DateTime(2018, 4, 30), Paid = false, Tax_rate = 0.13f, Items = ItemList },
        };


        // Return the Invoice List
        public static IEnumerable<Invoice> InvoiceList
        { get { return invoices; } }


        // Add new Invoice to List
        public static void AddInvoice(Invoice invoice)
        {
            invoice.Id = Repository.invoices.Count();
            invoices.Add(invoice);
        }

        // Remove Invoice from List
        public static void DeleteInvoice(Invoice invoice)
        {
            for (int i = 0; i < invoices.Count; i++)
            {
                if (invoices[i].Id == invoice.Id)
                {
                    invoices.Remove(invoices[i]);
                }
            }
        }

        // Return the Client List
        public static IEnumerable<Client> ClientList
        {
            get { return clients; }
        }

        public static List<Client> getAllClients()
        {
            return clients;
        }

        //return a list of users by email
        public static List<User> getAllUsers()
        {
            return new List<User>()
            {
                new User("Jim", "Jones", "jim@gmail.com", "hello"),
                new User("Lenin", "Moreno", "president@ecuador.gov", "i_will_never_die"),
                new User("Linus", "Techtips", "linus@youtube.com", "password"),
            };
        }
        public static User getUserByEmail(string Email)
        {
            foreach(User user in getAllUsers())
            {
                if(user.EmailAddress == Email)
                {
                    return user;
                }
            }
            return null;
        }
    }
}

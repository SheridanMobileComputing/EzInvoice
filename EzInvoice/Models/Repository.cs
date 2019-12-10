using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EzInvoice.Models;

namespace EzInvoice.Models
{
    public class Repository
    {
        private static EZInvoiceDB _context;

        public Repository(EZInvoiceDB context)
        {
            _context = context;
        }
        /*********************
         *     INVOICES
         *********************/
        //private IEnumerable<Invoice> getInvoices()
        //{
        //    return _context.Invoices.ToList();
        //}

        //private void insertInvoice(Invoice invoice)
        //{
        //    //TODO: Do some basic error checking.
        //    _context.Invoices.Add(invoice);
        //    _context.SaveChanges();
        //}


        private static List<Client> clients = new List<Client>
        {
            new Client { Id = 0, Name = "Jeremy Clark",  Street = "6969 Fake Street", City = "Oakville", Province = "Ontario", Postal_code = "X9X9X9X", Country = "Canada", Email = "jclark@sheridancollege.ca"},
            new Client { Id = 1, Name = "Apple Inc", Street = "1 Apple Park Way", City = "Cupertino", Province = "California", Postal_code = "95014", Country = "United States",Email = "apple@support.ca"},
            new Client { Id = 2, Name = "Richard Sackler", Street = "24 Oxycontin Rd", City = "Austin", Province = "Texas", Postal_code = "78652", Country = "United States", Email = "dicksack@tx.com"},
        };

 
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
            /* foreach(User user in getAllUsers())
             {
                 if(user.EmailAddress == Email)
                 {
                     return user;
                 }
             }
             return null;*/

            var user = _context.Users.FirstOrDefault(a => a.EmailAddress == Email);

            

           // var user = _context.Users.Find(Email);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}

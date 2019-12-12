using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class Repository
    {
        private EZInvoiceDB _context;

        public Repository(EZInvoiceDB context)
        {
            _context = context;
        }


        public static User getUserByEmail(string Email)
        {

            return null;
        }
    }
}

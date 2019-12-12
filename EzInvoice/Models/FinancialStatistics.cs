using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Models
{
    public class FinancialStatistics
    {
        //calculates a bunch of stats about the finances in our database.
        private EZInvoiceDB _context;
        public FinancialStatistics(EZInvoiceDB context)
        {
            this._context = context;
        }

        //amount owed per year.
        public double OwedAmount(int year)
        {
            double total = 0;
            for (int i = 0; i < 12; i++)
            {
                total += OwedAmount(year, i + 1);
            }

            return total;
        }

        //the amount of money we are owed for this month.
        public double OwedAmount(int year, int month)
        {
            //get all invoices due this month.
            var invoices = _context.Invoices
                .Include(i => i.InvoiceItems)
                .Where(i => i.DueDate.Year.CompareTo(year) == 0)
                .Where(i => i.DueDate.Month.CompareTo(month) == 0)
                .ToList();

            double total = 0;
            foreach (var invoice in invoices)
            {
                total += invoice.Total();
            }
            return total;
        }

        //amount paid per year.
        public double PaidAmount(int year)
        {
            double total = 0;
            for (int i = 0; i < 12; i++)
            {
                total += PaidAmount(year, i + 1);
            }

            return total;
        }

        //get the total balance of all paid invoices.
        public double PaidAmount(int year, int month)
        {
            //get all Paid invoices for this month.
            var invoices = _context.Invoices
                .Include(i => i.InvoiceItems)
                .Where(i => i.Paid.Equals(true))
                .Where(i => i.DueDate.Year.CompareTo(year) == 0)
                .Where(i => i.DueDate.Month.CompareTo(month) == 0)
                .ToList();
            double total = 0;

            foreach (var invoice in invoices)
            {
                total += invoice.Total();
            }
            return total;
        }

    }
}

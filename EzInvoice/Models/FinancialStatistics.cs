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
        private int year;
        public FinancialStatistics(EZInvoiceDB context, int year)
        {
            this._context = context;
            this.year = year;
        }

        public int getYear()
        {
            return this.year;
        }

        //amount owed per year.
        public double OwedAmount()
        {
            /*double total = 0;
            for (int i = 0; i < 12; i++)
            {
                total += OwedAmount(i + 1);
            }

            return total;*/
            return OwedAmount(12);
        }

        //the amount of money we are owed for this month.
        public double OwedAmount(int month)
        {
            //get all invoices due this month.
            var invoices = _context.Invoices
                .Include(i => i.InvoiceItems)
                .Where(i => i.DateOfIssue.Year.CompareTo(this.year) == 0)
                .Where(i => month - i.DateOfIssue.Month >= 0)
                .ToList();

            double total = 0;
            foreach (var invoice in invoices)
            {
                total += invoice.Total();
            }
            return total - this.PaidAmount(month);
        }

        //amount paid per year.
        public double PaidAmount()
        {
            /*double total = 0;
            for (int i = 0; i < 12; i++)
            {
                total += PaidAmount(i + 1);
            }

            return total;*/
            return PaidAmount(12);
        }

        //get the total balance of all paid invoices.
        public double PaidAmount( int month)
        {
            //get all Paid invoices for this month.
            var invoices = _context.Invoices
                .Include(i => i.InvoiceItems)
                .Where(i => i.Paid.Equals(true))
                .Where(i => i.DueDate.Year.CompareTo(this.year) == 0)
                .Where(i => month - i.DueDate.Month >= 0)
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

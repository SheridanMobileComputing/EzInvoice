using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EzInvoice.Controllers
{
    public class InvoiceController : Controller
    {
        private EZInvoiceDB _context;

        public InvoiceController(EZInvoiceDB context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View("InvoiceMain", _context.Invoices.Include(i => i.Client).ToList());
        }


        [HttpGet]
        public IActionResult CreateInvoice()
        {
            return View("InvoiceForm");
        }


        public async Task<IActionResult> EditInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("InvoiceForm", invoice);
        }


        [HttpPost]
        public async Task<IActionResult> SaveInvoice(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if (invoice.Id == null)
                {
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Invoice");
                }

                var InvoiceInDb = await _context.Invoices.FindAsync(invoice.Id);

                await TryUpdateModelAsync<Invoice>(
                    InvoiceInDb,
                    "",
                    i => i.Client,
                    i => i.DateOfIssue,
                    i => i.DueDate,
                    i => i.TaxRate,
                    i => i.InvoiceItems
                );
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Invoice");
            }
            return View("InvoiceForm");
        }


        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context
                .Invoices.Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.InvoiceItems.Count > 0)
            {
                foreach (var invoiceItems in invoice.InvoiceItems)
                {
                    // DELETE INVOICE ITEMS
                }
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Invoice");
        }


        public async Task<IActionResult> InvoiceDetail(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("InvoiceDetail", invoice);
        }


        public async Task<IActionResult> PayInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return View(NotFound());
            }
            return View("PayInvoice", invoice);
        }

    }
}
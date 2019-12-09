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
            return View("InvoiceMain", _context
                .Invoices.Include(i => i.Client).ToList());
        }


        [HttpGet]
        public IActionResult CreateInvoice()
        {
            ViewBag.Clients = _context
                .Clients.ToList();
            return View("InvoiceForm");
        }


        public async Task<IActionResult> EditInvoice(int id)
        {
            var invoice = await _context
                .Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("InvoiceEditForm", invoice);
        }



        [HttpPost]
        public async Task<IActionResult> SaveEditInvoice(Invoice invoice)
        {
            var InvoiceInDb = await _context
                .Invoices.FindAsync(invoice.Id);
            if (InvoiceInDb == null)
            {
                return NotFound();
            }

            await TryUpdateModelAsync<Invoice>(
                InvoiceInDb,
                "",
                i => i.DateOfIssue,
                i => i.DueDate,
                i => i.Paid,
                i => i.TaxRate
            );
            await _context.SaveChangesAsync();
            return View("InvoiceItemForm");
        }


        [HttpPost]
        public async Task<IActionResult> SaveInvoice(int clientId, DateTime dateOfIssue, DateTime dueDate, bool paid, float taxRate)
        {
            var client = await _context
                .Clients.FindAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }

            Invoice invoice = new Invoice
            {
                Client = client,
                DateOfIssue = dateOfIssue,
                DueDate = dueDate,
                Paid = paid,
                TaxRate = taxRate
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            
            return View("InvoiceItemForm", ViewBag.InvoiceId);
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
                foreach (var item in invoice.InvoiceItems)
                {
                    _context.InvoiceItems.Remove(item);
                }
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Invoice");
        }


        public async Task<IActionResult> InvoiceDetail(int id)
        {
            var invoice = await _context
                .Invoices.Include(c => c.Client)
                .ThenInclude(i => i.Invoices)
                .ThenInclude(i => i.InvoiceItems)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("InvoiceDetail", invoice);
        }


        public async Task<IActionResult> PayInvoice(int id)
        {
            var invoice = await _context
                .Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Paid = true;
            await _context.SaveChangesAsync();
            return View("PayInvoice", invoice);
        }


        [HttpGet]
        public IActionResult CreateItem()
        {
            ViewBag.Invoices = _context.Invoices.ToList();
            return View("InvoiceItemForm");
        }


        public async Task<IActionResult> EditItem(int id)
        {
            var invoiceItem = await _context
                .InvoiceItems.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return View("InvoiceItemForm", invoiceItem);
        }


        [HttpPost]
        public async Task<IActionResult> SaveItem(int InvoiceId, double Quantity, double Cost, string ItemNo, string ItemDescription )
        {
            var invoice = await _context
                .Invoices.Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.Id == InvoiceId);
            if (invoice == null)
            {
                return NotFound();
            }

            InvoiceItem invoiceItem = new InvoiceItem
            {
                ItemNo = ItemNo,
                ItemDescription = ItemDescription,
                Quantity = Quantity,
                Cost = Cost
            };

            invoice.InvoiceItems.Append(invoiceItem);
            _context.InvoiceItems.Add(invoiceItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var invoiceItem = await _context
                .InvoiceItems.FindAsync(id);

            if (invoiceItem == null)
            {
                return NotFound();
            }

            _context.InvoiceItems.Remove(invoiceItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Invoice");
        }

    }
}
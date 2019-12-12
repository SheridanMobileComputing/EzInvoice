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
        public async Task<IActionResult> SaveEditInvoice(int InvoiceId, int clientId, DateTime dateOfIssue, DateTime dueDate, bool paid, float taxRate)
        {
            var InvoiceInDb = await _context
                .Invoices.FindAsync(InvoiceId);

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

            return RedirectToAction("Index");
            //ViewBag.InvoiceId = InvoiceInDb.Id;
            //return RedirectToAction("InvoiceDetail", new { id = InvoiceInDb.Id });
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
            _context.SaveChanges(); //do this synchronously so we can get the id in the next line...

            return RedirectToAction("InvoiceDetail", new { id = invoice.Id });
            //return View("InvoiceDetail", invoice.Id);
        }

        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            foreach (var item in invoice.InvoiceItems)
            {
                _context.InvoiceItems.Remove(item);
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
        public IActionResult CreateItem(int id)
        {
            var invoice =  _context
                .Invoices.Find(id);

            if (invoice == null)
            {
                return NotFound();
            }

            ViewBag.InvoiceId = id;
            return View("InvoiceItemForm", new InvoiceItem());
        }


        public async Task<IActionResult> EditItem(int id)
        {
            var invoiceItem = await _context.InvoiceItems
                .Where(i => i.Id == id)
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync();

            if (invoiceItem == null)
            {
                return NotFound();
            }

            //var invoiceId = invoiceItem.Invoice.Id;

            //return RedirectToAction("InvoiceDetail", new { id = invoiceId });

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
                Cost = Cost,
                Invoice = invoice
            };

            invoice.InvoiceItems.Append(invoiceItem);
            _context.InvoiceItems.Add(invoiceItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("InvoiceDetail", new { id = InvoiceId });
        }
        
        public async Task<IActionResult> SaveEditItem(int ItemId, double Quantity, double Cost, string ItemNo, string ItemDescription)
        {
            //if we are editing an item...
            var item = await _context.InvoiceItems
                    .Include(i => i.Invoice)
                    .Where(i => i.Id == ItemId)
                    .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            await TryUpdateModelAsync<InvoiceItem>(
                item,
                "",
                i => i.Quantity,
                i => i.Cost,
                i => i.ItemNo,
                i => i.ItemDescription
            );
            await _context.SaveChangesAsync();

            return RedirectToAction("InvoiceDetail", new { id = item.Invoice.Id });

        }

        public IActionResult DeleteItem(int id)
        {
            var invoiceItem = _context.InvoiceItems
                .Include(i => i.Invoice)
                .Where(i => i.Id == id)
                .FirstOrDefault();

            var invoiceId = invoiceItem.Invoice.Id;

            if (invoiceItem == null)
            {
                return NotFound();
            }

            _context.InvoiceItems.Remove(invoiceItem);
            _context.SaveChanges();

            return RedirectToAction("InvoiceDetail", new { id = invoiceId });
            //return RedirectToAction("Index", "Invoice");
        }

    }
}
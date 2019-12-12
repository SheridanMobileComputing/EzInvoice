using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EzInvoice.Controllers
{
    public class ClientController : Controller
    {
        
        private EZInvoiceDB _context;

        public ClientController(EZInvoiceDB context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewBag.context = _context;
            return View("ClientMain", _context.Clients.ToList());
        }


        public IActionResult CreateClient()
        {
            return View("ClientForm");
        }


        public async Task<IActionResult> EditClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View("ClientForm", client);
        }


        [HttpPost]
        public async Task<IActionResult> SaveClient(Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.Id == null)
                {
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Client");
                }

                var clientInDb = await _context.Clients.FindAsync(client.Id);

                await TryUpdateModelAsync<Client>(
                    clientInDb,
                    "",
                    c => c.Name,
                    c => c.Email,
                    c => c.Street,
                    c => c.City,
                    c => c.Postal_code,
                    c => c.Province,
                    c => c.Country
                );
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Client");
            }
            return View("ClientForm");
        }


        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context
                .Clients.Include(cli => cli.Invoices)
                .ThenInclude(inv => inv.InvoiceItems)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            if (client.Invoices.Count > 0)
            {
                /*foreach (var invoice in client.Invoices)
                {
                    //:todo DELETE INVOICE ITEMS, THEN INVOICE
                    foreach (var item in client.Invoices.InvoiceItem)
                    {
                        _context.InvoiceItems.Remove(item);
                    }
                    _context.Invoices.Remove(invoice);
                }*/
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Client");
        }

    }
}
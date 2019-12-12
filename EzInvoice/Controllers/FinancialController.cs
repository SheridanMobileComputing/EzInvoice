using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;

namespace EzInvoice.Controllers
{
    public class FinancialController : Controller
    {

        private EZInvoiceDB _context;


        public FinancialController(EZInvoiceDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", new { year = 2019 });
        }

        [Route("/Financial/{year}")]
        public IActionResult Index(int year)
        {
            FinancialStatistics financialStats = new FinancialStatistics(_context, year);
            return View("FinancialMain", financialStats);
        }



    }
}
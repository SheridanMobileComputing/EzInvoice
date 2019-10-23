using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzInvoice.Controllers
{
    public class HomeController : Controller
    {
        //default route: "index" or "/"
        public IActionResult Index()
        {
            //check to see if the user has logged in already, if they have not:
            return Login();
            //else, direct to the main hub.
        }

        public IActionResult Login()
        {
            //an empty login attempt object will tell the View to render the Login page as
            //  if no attempt has yet been made.
            return View("Login", new LoginAttempt());
        }

        [HttpPost]
        public IActionResult Login(LoginAttempt attempt)
        {
            if(attempt.wasSuccessful())
            {
                return Dashboard();
            }

            return View("Login", attempt);
        }


        public IActionResult Signup()
        {
            return View();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View("Error", 404);
        }


        public IActionResult Dashboard()
        {
            return View("Dashboard");
        }


        public IActionResult InvoiceMain()
        {
            return View("InvoiceMain", Repository.InvoiceList);
        }

        [HttpGet]
        public IActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInvoice(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.Id = Repository.invoiceNo;
                Repository.invoiceNo += 1;
                Repository.AddInvoice(invoice);
                return View("Confirmation", invoice);
            }
            else
            {
                // there is a validation error              
                return View();
            }
        }


        public IActionResult EditInvoice(int id)
        {
            var request = Repository.InvoiceList.SingleOrDefault(r => r.Id == id);

            if (request == null)
            {
                return View("Error");
            }
            return View("EditInvoice", request);
        }


        public IActionResult InvoiceDetail(int id)
        {
            var request = Repository.InvoiceList.SingleOrDefault(r => r.Id == id);

            if (request == null)
            {
                return View("Error");
            }
            return View("InvoiceDetail", request);
        }

        public IActionResult PayInvoice(int id)
        {
            var request = Repository.InvoiceList.SingleOrDefault(r => r.Id == id);

            if (request == null)
            {
                return View("Error");
            }
            return View("PayInvoice", request);
        }
    }
}

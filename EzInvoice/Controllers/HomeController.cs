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
            //an empty loginattempt object will tell the View to render the Login page as
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
            return View("Signup");
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View("Error", 404);
        }

        public IActionResult Dashboard()
        {
            //the dashboard, for now, will show the top five invoices, by due date.
            var invoices = Invoice.GetInvoices();
            //order them here
            return View("Dashboard", invoices);
        }

        public IActionResult InvoiceMain()
        {
            return View("InvoiceMain");
        }
    }
}

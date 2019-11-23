using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EzInvoice.Controllers
{
    public class HomeController : Controller
    {
        private EZInvoiceDB _context; 

/*        public HomeController( EZInvoiceDB context)
        {
            _context = context;
        }*/
        private bool LoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("EmailAddress"));
        }
        //default route: "index" or "/"
        public IActionResult Index()
        {
            //if we're not logged in...
            if(!LoggedIn())
            {
                return Login();
            }
            else
            {
                return Dashboard();
            }
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
                HttpContext.Session.SetString("EmailAddress", attempt.Email_address);
                return Dashboard();
            }

            return View("Login", attempt);
        }

        public IActionResult Logout()
        {
            if (LoggedIn())
            {
                HttpContext.Session.Clear();
            }
            return Index();
        }


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(User user)
        {
            var dcontext = new EZInvoiceDB();

            dcontext.Users.Add(user);
      
            await dcontext.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }
        public IActionResult MyAccount()
        {
            if(LoggedIn())
            {
                User activeUser = Repository.getUserByEmail(HttpContext.Session.GetString("EmailAddress"));
                if(activeUser != null)
                {
                    return View("AccountInfo", activeUser);
                }
            }
            return Login();
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

                Repository.AddInvoice(invoice);
                
                return View("InvoiceCreationConfirmation", invoice);
            }
            else
            {
                // there is a validation error              
                return View();
            }
        }

        public IActionResult DeleteInvoice(int id)
        {
            var request = Repository.InvoiceList.SingleOrDefault(r => r.Id == id);

            if (request == null)
            {
                return View("Error");
            }
            return View("DeleteInvoice", request);
        }

        [HttpPost]
        public IActionResult DeleteInvoice(Invoice invoice)
        {
            Repository.DeleteInvoice(invoice);
            return View("InvoiceDeletionConfirmation");
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

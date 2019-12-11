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

        public HomeController(EZInvoiceDB context)
        {
            _context = context;
        }

        private bool LoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("EmailAddress"));
        }

        //default route: "index" or "/"
        public IActionResult Index()
        {
            if(!LoggedIn())
            {
                return Login();
            }
            else
            {
                return View("Dashboard");
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
            return View(new SignupAttempt());
        }

        [HttpPost]
        public IActionResult Signup(SignupAttempt signupAttempt)
        {
            signupAttempt.ErrorMessage = signupAttempt.getError();
            if (signupAttempt.ErrorMessage == "")
            {
                if(_context.Users.Count() > 0)
                {
                    //Ensure this signup info hasn't already been saved to the DB.
                    var user = _context.Users
                    .FirstOrDefault(s => s.EmailAddress == signupAttempt.EmailAddress);
                    if (user != null)
                    {
                        signupAttempt.ErrorMessage = "User with this email already exists.";
                        return View(signupAttempt);
                    }
                }

                //save to db
                _context.Users.Add(new User(signupAttempt));
                _context.SaveChangesAsync();
                return Login();
            }
            else
            {
                return View(signupAttempt);
            }
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
            var user = new User("bob", "gong", "bob@hotmail.com", "123456");
            // return Error403(); 
            return View("AccountInfo", user);
        }


        [Route("error/404")]
        public IActionResult Error404()
        {
            return View("Error", 404);
        } 

        [Route("error/403")]
        public IActionResult Error403()
        {
            return View("Error", 403);
        }

        public IActionResult Dashboard()
        {
            if (LoggedIn()) {
                return View("Dashboard");
            }
            return Error403();
        }

    }
}

﻿using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            // return View("Login", new LoginAttempt());
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(User login)
        {
            if (ModelState.IsValid)
            {
                var user = await _context
                    .Users
                    .FirstOrDefaultAsync(s => s.EmailAddress == login.EmailAddress);

                if (user == null)
                {
                    return View("Login");
                }

                HttpContext.Session.SetString("EmailAddress", user.EmailAddress);
                return Dashboard();
            }

            return View(login);

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
        public async Task<IActionResult> Signup(SignupAttempt signupattempt)
        {
            ViewBag.ErrorMsg = "";
            if (ModelState.IsValid)
            {

                if (_context.Users.Count() > 0)
                {
                    //Ensure this signup info hasn't already been saved to the DB.
                    var user = _context.Users
                    .FirstOrDefault(s => s.EmailAddress == signupattempt.EmailAddress);
                    if (user != null)
                    {
                        ViewBag.ErrorMsg = "User with this email already exists";
                        return View(signupattempt);
                    }
                }
                var newUser = new User()
                {
                    FirstName = signupattempt.FirstName,
                    LastName = signupattempt.LastName,
                    EmailAddress = signupattempt.EmailAddress,
                    Password = signupattempt.Password
                };
                //save to db
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Login();
            }
            return View(signupattempt);
        }
        
        public async Task<IActionResult> MyAccount()
        {
            if(LoggedIn())
            {
                var activeUser =  await _context.Users.FirstOrDefaultAsync(a => a.EmailAddress == HttpContext.Session.GetString("EmailAddress"));
                if(activeUser != null)
                {
                    return View("AccountInfo", activeUser);
                }
            }
            return Error403();
        }

        public IActionResult Dash()
        {
            var user = HttpContext.Session.GetString("EmailAddress");

            /* ViewBag.clients = _context
                 .Users
                 .Include(u => u.Clients)
             //ViewBag.invoices = _context.Invoices.ToList();

             return View(_context.Invoices.ToList());*/
         
            return null;
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

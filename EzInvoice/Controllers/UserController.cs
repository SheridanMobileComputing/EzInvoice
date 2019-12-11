using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Mvc;

namespace EzInvoice.Controllers
{
    public class UserController : Controller
    {

        private EZInvoiceDB _context;


        public UserController(EZInvoiceDB context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var user = new User("bob", "gong", "bob@hotmail.com", "123456");
            return View("AccountInfo", user);
        }

        public IActionResult Edit()
        {
           

            return View("EditUser");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var email = HttpContext.Session.GetString("EmailAddress");

            var user = _context.Users
                .FirstOrDefault(s => s.EmailAddress == email);
            /* if (user == null)
             {
                 return RedirectToAction("Home", "Error403", 403);
             }
             return View("AccountInfo", user);*/

            return View("AccountInfo", user);
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            // takes a parameter of user and populates the user form with the model attributres 
            if(user == null)
            {
                return NotFound();
            }
        
            return View("EditUser", user);
        }


        public async Task<IActionResult> Update(User user)
        {
            
            var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress);

            await TryUpdateModelAsync<User>(
                userInDb,
                "",
                u => u.FirstName, u => u.LastName, u => u.EmailAddress
                );

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "User");
        }


    }
}
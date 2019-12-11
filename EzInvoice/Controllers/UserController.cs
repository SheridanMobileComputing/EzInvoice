using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
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
            var user = new User("bob", "gong", "bob@hotmail.com", "123456");
            return View("AccountInfo", user);
        }

        public IActionResult EditUser()
        {
        
            return View();
        }


        public async Task<IActionResult> Update(User user)
        {
            var email = "kevin@hotmail.com";

            var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);

            await TryUpdateModelAsync<User>(
                userInDb,
                "",
                u => u.FirstName, u => u.LastName, u => u.EmailAddress
                );

            await _context.SaveChangesAsync();

            return RedirectToAction("UserMain", "User");
        }
    }
}
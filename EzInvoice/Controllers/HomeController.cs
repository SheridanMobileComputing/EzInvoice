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
            return View("Login");
            //else, direct to the main hub.
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [Route("signup")]
        public IActionResult Signup()
        {
            return View("Signup");
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View("Error", 404);
        }
    }
}

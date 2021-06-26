using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string role)
        {
            
            return View(role);
        }
        public IActionResult ColorCards()
        {
            return View();
        }
    }
}

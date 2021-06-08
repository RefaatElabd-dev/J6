using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Areas.BasicInfo.Controllers
{

    [Area("BasicInfo")]
    public class InventoryDefinationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

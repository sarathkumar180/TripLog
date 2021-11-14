using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripLog.Controllers
{
    public class DestinationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

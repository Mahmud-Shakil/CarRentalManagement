using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_5.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMassage = "Page Not Found";
                    break;
                default:
                    ViewBag.ErrorMassage = "Some Error Occored. Please Contact system Administator";
                    break;
            }
            return View("Error");
        }
    }
}

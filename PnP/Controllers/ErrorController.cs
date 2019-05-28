using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PnP.Controllers
{
  
  public class ErrorController : Controller
  {
    // GET: /<controller>/
    public IActionResult Error404()
    {
      return View("Error404");
    }
  }
}

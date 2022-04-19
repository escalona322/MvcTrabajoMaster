using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class ApuntadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

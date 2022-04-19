using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class SetsController : Controller
    {
        private ServiceApiTorneos service;

        public SetsController(ServiceApiTorneos service)
        {
            this.service = service;          
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditarSet(int idset, int idtorneo)
        {
         
        }
        [HttpPost]
        public IActionResult EditarSet(int idset, int ap1,
            int ap2, int apganador, string resultado,
            string ronda, int idtorneo)
        {
         
        }

        public IActionResult NuevoSet(int idtorneo)
        {
          
        }

        [HttpPost]
        public IActionResult NuevoSet(int ap1, int ap2,
            int apganador, string resultado, string ronda,
            int idtorneo)
        {
          
        }

        public IActionResult ListaSetsApuntado(int idap)
        {
        
        }
        public IActionResult ListaSetsApuntadoAdmin(int idap)
        {
     
        }
    }
}

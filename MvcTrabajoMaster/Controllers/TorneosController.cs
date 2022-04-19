using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class TorneosController : Controller
    {

        private ServiceApiTorneos service;
        private ServiceStorageBlobs serviceBlobs;

        public TorneosController(ServiceApiTorneos service, ServiceStorageBlobs serviceBlobs)
        {
            this.service = service;
            this.serviceBlobs = serviceBlobs;
        }
        public IActionResult ListaTorneos(int? posicion)
        {
           
        }

        public IActionResult ListaTorneosAdmin(int? posicion)
        {
           
        }
        public IActionResult DetallesTorneo(int idtorneo)
        {
         
        }

        public IActionResult NuevoTorneo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoTorneo(string nombre, string region,
            DateTime fecha, int napuntados, string descripcion,
            string normas, string tipo, string link, IFormFile foto)
        {
           
        }

        public IActionResult EliminarTorneo(int idtorneo)
        {
        
        }

        public IActionResult EditarTorneo(int idtorneo)
        {
          
        }

        [HttpPost]
        public async Task<IActionResult> EditarTorneo(int idtorneo, string nombre,
            string region, DateTime fecha, int napuntados,
            string descripcion, string normas, string tipo,
            string link, IFormFile foto)
        {
          
        }


    }
}

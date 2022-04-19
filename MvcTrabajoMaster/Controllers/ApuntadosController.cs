using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class ApuntadosController : Controller
    {

        private ServiceApiTorneos service;

        public ApuntadosController(ServiceApiTorneos service)
        {
            this.service = service;
        }
        public IActionResult ListaApuntadosTorneo(int idtorneo, int? posicion)
        {

            
        }
        public IActionResult ListaApuntadosAdmin(int? idtorneo)
        {
         

        }
 
        public IActionResult NuevoApuntado(int idtorneo)
        {
         
        }

        [HttpPost]
        public IActionResult NuevoApuntado(
            int idtorneo, int idjugador, int puesto,
            string record, int seed)
        {
        
        }
        public IActionResult EditarApuntado(int idapuntado)
        {
           
        }

        [HttpPost]
        public IActionResult EditarApuntado(int idinscripcion,
            int idtorneo, int idjugador, int puesto,
            string record, int seed)
        {
           
        }

        public IActionResult EliminarApuntado(int idapuntado)
        {
        
        }
    }
}

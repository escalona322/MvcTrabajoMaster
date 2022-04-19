using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class JugadoresController : Controller
    {
        private ServiceApiTorneos service;

        public JugadoresController(ServiceApiTorneos service)
        {
            this.service = service;
        }
        public IActionResult ListaJugadores(int? posicion)
        {

           
        }

        public IActionResult ListaJugadoresAdmin(int? posicion)
        {

        
        }

        public IActionResult PerfilJugador(int idjugador)
        {
          
        }

       
        public IActionResult MiPerfil()
        {
           
           
        }

        public IActionResult NuevoJugador()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NuevoJugador(string nick,
            string region, string nombre, string email, string password,
            string rol, string equipo)
        {
          
        }
        public IActionResult EliminarJugador(int idjugador)
        {
      
        }
        public IActionResult EditarJugador(int idjugador)
        {
          
        }

        [HttpPost]
        public IActionResult EditarJugador(int idjugador,
            string nick, string region, string nombre,
            string email, string rol, string equipo)
        {
           
        }
    }
}

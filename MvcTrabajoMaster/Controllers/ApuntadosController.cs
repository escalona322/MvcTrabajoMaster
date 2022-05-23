using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Filters;
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
        public async Task<IActionResult> ListaApuntadosTorneo(int idtorneo, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ViewData["IDTORNEO"] = idtorneo;
            int Napuntados = await this.service.GetNApuntadosTorneoAsync(idtorneo);
            ViewData["NAPUNTADOS"] = Napuntados;
            int siguiente = posicion.Value + 20;
            int anterior = posicion.Value - 20;
            if (siguiente >= Napuntados)
            {
                siguiente = Napuntados;
            }
            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            List<VistaApuntadosTorneo> apuntados = 
                await this.service.GetVApuntadosByTorneoNoPagAsync(idtorneo);

            return View(apuntados);

        }
        public async Task<IActionResult> ListaApuntadosAdmin(int? idtorneo)
        {

            if (idtorneo == null)
            {
                List<VistaApuntadosTorneo> apuntados = await this.service.GetVApuntadosAsync();
                return View(apuntados);
            }
            else
            {
                List<VistaApuntadosTorneo> apuntadostor = await this.service.GetVApuntadosByTorneoNoPagAsync(idtorneo.Value);
                return View(apuntadostor);
            }
        }
    
        [AuthorizeJugadores]
        public async Task<IActionResult> NuevoApuntado(int idtorneo)
        {
            Torneo torneo = await this.service.GetTorneoByIdAsync(idtorneo);
            return View(torneo);
        }

        [HttpPost]
        public async Task<IActionResult> NuevoApuntado(
            int idtorneo, int idjugador, int puesto,
            string record, int seed)
        {
            int idapMax = await this.service.GetApuntadoMaxIdAsync();
            Apuntado ap = new Apuntado()
            {
                IdApuntado = idapMax,
                IdTorneo = idtorneo,
                IdJugador = idjugador,
                Puesto = puesto,
                Record = record,
                Seed = seed
            };
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.InsertApuntadoAsync(ap, token);
            await this.service.SumarApuntadoAsync(idtorneo);
            return RedirectToAction("ListaApuntadosTorneo",
                new { idtorneo }
                );
        }
        public async Task<IActionResult> EditarApuntado(int idapuntado)
        {
            Apuntado apuntadoEditar = await this.service.GetApuntadoByIdAsync(idapuntado);
            return View(apuntadoEditar);
        }

        [HttpPost]
        public async Task<IActionResult> EditarApuntado(int idinscripcion,
            int idtorneo, int idjugador, int puesto,
            string record, int seed)
        {
            Apuntado ap = new Apuntado()
            {
                IdApuntado = idinscripcion,
                IdTorneo = idtorneo,
                IdJugador = idjugador,
                Puesto = puesto,
                Record = record,
                Seed = seed
            };
            await this.service.UpdateApuntadoAsync(ap);
            return RedirectToAction("ListaApuntadosAdmin");
        }

        public async Task<IActionResult> EliminarApuntado(int idapuntado)
        {
            await this.service.DeleteApuntadoAsync(idapuntado);
            return RedirectToAction("ListaTorneosAdmin", controllerName: "Torneos");
        }
    }
}

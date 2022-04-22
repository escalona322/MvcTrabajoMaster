using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using NuggetModelsPryectoJalt;
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

        public async Task<IActionResult> EditarSet(int idset, int idtorneo)
        {
            Set setEditar = await this.service.GetSetByIdAsync(idset);
            List<VistaApuntadosTorneo> apuntados = await this.service.GetVApuntadosByTorneoNoPagAsync(idtorneo);
            ViewData["RONDA"] = setEditar.Ronda;
            ViewData["RESULTADO"] = setEditar.Resultado;
            ViewData["IDTORNEO"] = idtorneo;
            return View(apuntados);
        }
        [HttpPost]
        public async Task<IActionResult> EditarSet(int idset, int ap1,
            int ap2, int apganador, string resultado,
            string ronda, int idtorneo)
        {
            Set set = new Set()
            {
                IdSet = idset,
                IdApuntado1 = ap1,
                IdApuntado2 = ap2,
                Ganador = apganador,
                Resultado = resultado,
                Ronda = ronda,
                IdTorneo = idtorneo
            };
            await this.service.UpdateSetAsync(set);
            return RedirectToAction("ListaSetsApuntadoAdmin",
                new { idap = apganador }
               );
        }

        public async Task<IActionResult> NuevoSet(int idtorneo)
        {
            ViewData["IDTORNEO"] = idtorneo;
            List<VistaApuntadosTorneo> apuntados = await this.service.GetVApuntadosByTorneoNoPagAsync(idtorneo);
            return View(apuntados);
        }

        [HttpPost]
        public async Task<IActionResult> NuevoSet(int ap1, int ap2,
            int apganador, string resultado, string ronda,
            int idtorneo)
        {
            int idsetMax = await this.service.GetSetMaxIdAsync();
            Set set = new Set()
            {
                IdSet = idsetMax,
                IdApuntado1 = ap1,
                IdApuntado2 = ap2,
                Ganador = apganador,
                Resultado = resultado,
                Ronda = ronda,
                IdTorneo = idtorneo
            };
            return RedirectToAction("ListaSetsApuntado",
                 new { idap = apganador }
                );
        }

        public async Task<IActionResult> ListaSetsApuntado(int idap)
        {
            List<VistaSetFormateado> vistaSets = await this.service.GetSetsFormatByIdApuntadoAsync(idap);
            return View(vistaSets);
        }
        public async Task<IActionResult> ListaSetsApuntadoAdmin(int idap)
        {
            List<VistaSetFormateado> vistaSets = await this.service.GetSetsFormatByIdApuntadoAsync(idap);
            return View(vistaSets);
        }
    }
}

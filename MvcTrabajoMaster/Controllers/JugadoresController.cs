using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Filters;
using MvcTrabajoMaster.Services;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> ListaJugadores(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }


            int NJugadores = await this.service.GetNJugadoresAsync();
            ViewData["NJUGADORES"] = NJugadores;
            int siguiente = posicion.Value + 25;
            int anterior = posicion.Value - 25;
            if (siguiente >= NJugadores)
            {
                siguiente = NJugadores;
            }
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["POSICION"] = posicion;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            List<VistaJugadores> Jugadores = await this.service.GetVistaJugadoresAsync(posicion.Value);
            return View(Jugadores);
        }

        public async Task<IActionResult> ListaJugadoresAdmin(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }


            int NJugadores = await this.service.GetNJugadoresAsync();
            ViewData["NJUGADORES"] = NJugadores;
            int siguiente = posicion.Value + 25;
            int anterior = posicion.Value - 25;
            if (siguiente >= NJugadores)
            {
                siguiente = NJugadores;
            }
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["POSICION"] = posicion;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            List<VistaJugadores> Jugadores = await this.service.GetVistaJugadoresAsync(posicion.Value);
            return View(Jugadores);
        }

        public async Task<IActionResult> PerfilJugador(int idjugador)
        {
            List<Torneo> TorneosJugador = await this.service.GetTorneosByIdJugadorAsync(idjugador);
            List<VistaSetFormateado> Sets = await this.service.GetSetsFormatByIdJugadorAsync(idjugador);
            Jugador jug = await this.service.GetJugadorByIdAsync(idjugador);
            ViewData["SETS"] = Sets;
            ViewData["TORNEOS"] = TorneosJugador;
            int setganados = 0;
            foreach (VistaSetFormateado set in Sets)
            {
                if (set.NickGanador == jug.Nick)
                {
                    setganados += 1;
                }
            }
            ViewData["SETS"] = Sets;
            ViewData["TORNEOS"] = TorneosJugador;
            ViewData["SETSGANADOS"] = setganados;
            ViewData["NUMSETS"] = Sets.Count();
            double winrate = ((double)setganados / (double)Sets.Count()) * 100;

            ViewData["WINRATE"] = winrate;
            return View(jug);
        }

        [AuthorizeJugadores]
        public async Task<IActionResult> MiPerfil()
        {
            int idjugador = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Torneo> TorneosJugador = await this.service.GetTorneosByIdJugadorAsync(idjugador);
            List<VistaSetFormateado> Sets = await this.service.GetSetsFormatByIdJugadorAsync(idjugador);

            Jugador jug = await this.service.GetJugadorByIdAsync(idjugador);
            int setganados = 0;
            foreach (VistaSetFormateado set in Sets)
            {
                if (set.NickGanador == jug.Nick)
                {
                    setganados += 1;
                }
            }
            ViewData["SETS"] = Sets;
            ViewData["TORNEOS"] = TorneosJugador;
            ViewData["SETSGANADOS"] = setganados;
            ViewData["NUMSETS"] = Sets.Count();
            double winrate = ((double)setganados / (double)Sets.Count()) * 100;

            ViewData["WINRATE"] = winrate;
            return View(jug);
        }

        public IActionResult NuevoJugador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NuevoJugador(string nick,
            string region, string nombre, string email, string password,
            string rol, string equipo)
        {
            int idjugMax = await this.service.GetJugadorMaxIdAsync();

            Jugador jug = new Jugador()
            {
                IdJugador = idjugMax,
                Nick = nick,
                Region = region,
                Nombre = nombre,
                Email = email,
                Password = password,
                Rol = rol,
                Equipo = equipo
            };
            await this.service.InsertJugadorAsync(jug);
            return RedirectToAction("MiPerfil");
        }

        public async Task<IActionResult> EliminarJugador(int idjugador)
        {
            await this.service.DeleteJugadorAsync(idjugador);
            return RedirectToAction("ListaJugadoresAdmin");
        }
        public async Task<IActionResult> EditarJugador(int idjugador)
        {
            Jugador jugadorEditar = await this.service.GetJugadorByIdAsync(idjugador);
            return View(jugadorEditar);
        }

        [HttpPost]
        public async Task<IActionResult> EditarJugador(int idjugador,
            string nick, string region, string nombre,
            string email, string rol, string equipo)
        {

            Jugador jug = new Jugador()
            {
                IdJugador = idjugador,
                Nick = nick,
                Region = region,
                Nombre = nombre,
                Email = email,               
                Rol = rol,
                Equipo = equipo
            };

            await this.service.UpdateJugadorAsync(jug);
            return RedirectToAction("ListaJugadores");
        }
    }
}

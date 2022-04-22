using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Filters;
using MvcTrabajoMaster.Services;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.IO;
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
        [AuthorizeJugadores]
        public async Task<IActionResult> ListaTorneos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int MaxRegistro = await this.service.GetNumeroTorneosAsync();
            ViewData["REGISTROS"] = MaxRegistro;
            int siguiente = posicion.Value + 1;
            if (siguiente > MaxRegistro)
            {
                siguiente = MaxRegistro;
                ViewData["MENSAJE"] = "Este es el ultimo torneo";
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
                ViewData["MENSAJE"] = "Este es el primer primer torneo";
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;

            List<VistaTorneo> torneos = await this.service.GetTorneosPaginadoAsync(posicion.Value);
            return View(torneos);
        }

        public async Task<IActionResult> ListaTorneosAdmin(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int MaxRegistro = await this.service.GetNumeroTorneosAsync();
            ViewData["REGISTROS"] = MaxRegistro;
            int siguiente = posicion.Value + 1;
            if (siguiente > MaxRegistro)
            {
                siguiente = MaxRegistro;
                ViewData["MENSAJE"] = "Este es el ultimo torneo";
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
                ViewData["MENSAJE"] = "Este es el primer primer torneo";
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            List<VistaTorneo> torneos = await this.service.GetTorneosPaginadoAsync(posicion.Value);
            return View(torneos);
        }
        public async Task<IActionResult> DetallesTorneo(int idtorneo)
        {
            List<VistaApuntadosTorneo> apuntados = await this.service.GetVApuntadosByTorneoNoPagAsync(idtorneo);
            Torneo torneo = await this.service.GetTorneoByIdAsync(idtorneo);
            ViewData["TORNEO"] = torneo;
            return View(apuntados);
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
            int idtorneoMax = await this.service.GetTorneoMaxIdAsync();


            string containerName = "torneos";
            string blobName = foto.FileName;
            using (Stream stream = foto.OpenReadStream())
            {
                await this.serviceBlobs.UploadBlobAsync(containerName, blobName, stream);
            }
            Torneo torneo = new Torneo()
            {
                IdTorneo = idtorneoMax,
                Nombre = nombre,
                Region = region,
                Fecha = fecha, 
                Napuntados = napuntados,
                Descripcion = descripcion,
                Normas = normas,
                Tipo = tipo,
                Link = link,
                Foto = blobName
            };

           await this.service.InsertTorneoAsync(torneo);
            return RedirectToAction("ListaTorneos");
        }
    

        public async Task<IActionResult> EliminarTorneo(int idtorneo)
        {
             await this.service.DeleteTorneo(idtorneo);
             return RedirectToAction("ListaTorneosAdmin");
        }

        public async Task<IActionResult> EditarTorneo(int idtorneo)
        {
            Torneo torneo = await this.service.GetTorneoByIdAsync(idtorneo);
            return View(torneo);
        }

        [HttpPost]
        public async Task<IActionResult> EditarTorneo(int idtorneo, string nombre,
            string region, DateTime fecha, int napuntados,
            string descripcion, string normas, string tipo,
            string link, IFormFile foto)
       {

            //string path = await this.helperUpload.UploadFileAsync(foto, Folders.Images);
            //this.repo.UpdateTorneo(idtorneo, nombre,
            //region, fecha, napuntados, descripcion,
            //normas, tipo, link, path);

            return RedirectToAction("ListaTorneos");
        }


    }
}

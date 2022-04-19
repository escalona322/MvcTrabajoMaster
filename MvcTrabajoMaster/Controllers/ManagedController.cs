using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcTrabajoMaster.Services;
using NuggetModelsPryectoJalt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcTrabajoMaster.Controllers
{
    public class ManagedController : Controller
    {
        private ServiceApiTorneos service;

        public ManagedController(ServiceApiTorneos service)
        {
            this.service = service;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            Jugador jug =
                this.repo.ExisteJugador(email, password);
            if (jug != null)
            {
                ClaimsIdentity identity =
                new ClaimsIdentity
                (CookieAuthenticationDefaults.AuthenticationScheme
                , ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, jug.Email);
                identity.AddClaim(claimName);
                Claim claimId = new Claim
                    (ClaimTypes.NameIdentifier, jug.IdJugador.ToString());
                Claim claimRole = new Claim
                    (ClaimTypes.Role, jug.Rol);
                Claim claimRegion = new Claim
                    ("Region", jug.Region);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimRegion);
                ClaimsPrincipal userPrincipal =
                new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                (CookieAuthenticationDefaults.AuthenticationScheme
                , userPrincipal);
                string controller = HttpContext.Request.Cookies["controller"];
                string action = HttpContext.Request.Cookies["action"];
                HttpContext.Session.SetString("Rol", jug.Rol);


                return RedirectToAction(action, controller);

            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            return View();

        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
            (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

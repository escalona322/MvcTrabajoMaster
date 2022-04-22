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
        public async Task<IActionResult> LogIn(string username, string password)
        {
            string token =
            await this.service.GetTokenAsync(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                //SI EL USUARIO EXISTE, ALMACENAMOS EL TOKEN EN SESSION
                Jugador usu = await this.service.GetPerfilUsuario(token);
                ClaimsIdentity identity =
                new ClaimsIdentity
                (CookieAuthenticationDefaults.AuthenticationScheme
                , ClaimTypes.Name, ClaimTypes.NameIdentifier);
                identity.AddClaim(new Claim(ClaimTypes.Name, usu.Nick.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usu.IdJugador.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, usu.Rol.ToString()));
                identity.AddClaim(new Claim("TOKEN", token));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                (CookieAuthenticationDefaults.AuthenticationScheme
                , principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
            (CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("TOKEN");
            return RedirectToAction("Index", "Home");
        }
    }
}

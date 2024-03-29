﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;
using System.Security.Claims;

namespace MVC_CORE_EF_1.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public LoginController(ApplicationDbContext db, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _db = db;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            string hashedPassword = PasswordManager.HashPassword(model.Password);
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == hashedPassword);

            if (user == null)
            {
                TempData["error"] = "Account non esistente";
                return View();
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            TempData["success"] = "Login effettuato con successo";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["success"] = "Sei stato disconnesso";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Username,Password,TipoCliente,Nominativo,CodiceFiscale,PartitaIva")] User user)
        {
            if ((user.TipoCliente.ToLower() == "azienda" || user.TipoCliente.ToLower() == "privato") && ((user.CodiceFiscale.IsNullOrEmpty() && user.PartitaIva != null) || (user.PartitaIva.IsNullOrEmpty() && user.CodiceFiscale != null)))
            {
                try
                {
                    string HashedPassword = PasswordManager.HashPassword(user.Password);
                    user.Password = HashedPassword;
                    _db.Users.Add(user);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "L'username è già stato utilizzato";
                    ModelState.AddModelError("Exception", ex.Message);
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("TipoCliente", "Il tipo cliente deve essere azienda o privato");
                return View(user);
            }
        }


        public async Task<IActionResult> CheckUsername(string username)
        {
            bool doesUsernameExist = await _db.Users.AnyAsync(user => user.Username == username);
            return Json(!doesUsernameExist);
        }
    }
}

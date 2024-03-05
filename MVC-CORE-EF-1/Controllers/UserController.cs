using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;

namespace MVC_CORE_EF_1.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public UserController(ApplicationDbContext db, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _db = db;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public IActionResult Index()
        {
            List<User> users = new List<User>();
            users = _db.Users.ToList();

            return View(users);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid && (user.TipoCliente.ToLower() == "azienda" || user.TipoCliente.ToLower() == "privato") && ((user.CodiceFiscale.IsNullOrEmpty() && user.PartitaIva != null) || (user.PartitaIva.IsNullOrEmpty() && user.CodiceFiscale != null)))
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

        public IActionResult EditUser(int id)
        {
            User user = _db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid && (user.TipoCliente.ToLower() == "azienda" || user.TipoCliente.ToLower() == "privato") && ((user.CodiceFiscale.IsNullOrEmpty() && user.PartitaIva != null) || (user.PartitaIva.IsNullOrEmpty() && user.CodiceFiscale != null)))
            {


                try
                {
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    TempData["Success"] = $"{user.Nominativo} è stato modificato con successo";
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
                return View(user);
            }
        }
    }
}

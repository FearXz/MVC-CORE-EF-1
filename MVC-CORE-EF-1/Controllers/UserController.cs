using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;

namespace MVC_CORE_EF_1.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public object JsonRequestBehavior { get; private set; }

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
        public IActionResult EditUser(int id)
        {
            User user = _db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {

            try
            {
                var existingUser = _db.Users.Find(user.IdUser);
                if (existingUser == null)
                {
                    return NotFound();
                }
                existingUser.Username = user.Username;
                existingUser.Nominativo = user.Nominativo;
                existingUser.Role = user.Role;
                existingUser.TipoCliente = user.TipoCliente;
                existingUser.CodiceFiscale = user.CodiceFiscale;
                existingUser.PartitaIva = user.PartitaIva;

                _db.Users.Update(existingUser);
                _db.SaveChanges();
                TempData["Success"] = $"{user.Nominativo} è stato modificato con successo";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ModelState.AddModelError("Exception", ex.Message);
                return View(user);
            }
        }
    }
}

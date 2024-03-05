using Microsoft.AspNetCore.Mvc;

namespace MVC_CORE_EF_1.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
    }
}

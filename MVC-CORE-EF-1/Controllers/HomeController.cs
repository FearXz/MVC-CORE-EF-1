using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MVC_CORE_EF_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SearchShipping()
        {
            var listaSpedizioniUtente = await _db.Shippings.Where(s => s.IdUser == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToListAsync();
            return View(listaSpedizioniUtente);
        }
        [Authorize]
        public async Task<IActionResult> SearchShippingDetails(int id)
        {
            var listaDettagliSpedizione = await _db.ShippingDetails.Where(s => s.IdSpedizione == id).ToListAsync();
            return View(listaDettagliSpedizione);
        }

        public async Task<IActionResult> FetchShippingToday()
        {
            try
            {
                var listaSpedizioniOggi = await _db.Shippings.Where(s => s.DataConsegna.Date == DateTime.Today).ToListAsync();
                return Json(listaSpedizioniOggi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle spedizioni di oggi");
                return StatusCode(500, "Errore interno del server");
            }

        }
        public async Task<IActionResult> FetchInConsegna()
        {
            try
            {
                var numeroSpedizioniInConsegna =
                    await _db.Shippings
                    .Include(s => s.ShippingDetails)
                    .Where(s => s.ShippingDetails
                    .Any(sd => sd.StatoSpedizione == "In Consegna")).CountAsync();
                return Json(numeroSpedizioniInConsegna);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle spedizioni in consegna");
                return StatusCode(500, "Errore interno del server");
            }
        }
        // Conoscere il numero totale delle spedizioni memorizzate raggruppate per luogo di destinazione
        public async Task<IActionResult> FetchPerDestinazione()
        {
            try
            {
                var listaPerDestinazione =
                    await _db.Shippings
                    .GroupBy(s => s.IndirizzoDestinatario)
                    .Select(g => new { Destinazione = g.Key, NumeroSpedizioni = g.Count() })
                    .ToListAsync();
                return Json(listaPerDestinazione);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero delle spedizioni raggruppate per destinazione");
                return StatusCode(500, "Errore interno del server");
            }
        }

        public IActionResult NOPE()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

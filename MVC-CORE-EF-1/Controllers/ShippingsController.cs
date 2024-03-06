using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;

namespace MVC_CORE_EF_1.Controllers
{
    public class ShippingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shippings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Shippings.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shippings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings
                .Include(s => s.User)
                .Include(s => s.ShippingDetails)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // GET: Shippings/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Nominativo");
            return View();
        }

        // POST: Shippings/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUser,DataSpedizione,PesoSpedizione,IndirizzoDestinatario,NominativoDestinatario,CostoSpedizione,DataConsegna")] Shipping shipping)
        {

            _context.Add(shipping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Shippings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "Nominativo", shipping.IdUser);
            return View(shipping);
        }

        // POST: Shippings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSpedizione,IdUser,DataSpedizione,PesoSpedizione,IndirizzoDestinatario,NominativoDestinatario,CostoSpedizione,DataConsegna")] Shipping shipping)
        {

            try
            {
                _context.Update(shipping);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingExists(shipping.IdSpedizione))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Shippings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await _context.Shippings
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // POST: Shippings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping != null)
            {
                _context.Shippings.Remove(shipping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateStatus(int idSpedizione)
        {

            ViewBag.IdSpedizione = idSpedizione;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStatus(int idSpedizione, [Bind("idSpedizione,StatoSpedizione,LuogoCorrente,NoteSpedizione,DataAggiornamento")] ShippingDetail shippingDetail)
        {
            try
            {
                shippingDetail.IdSpedizione = idSpedizione;

                _context.Add(shippingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = idSpedizione });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Details", new { id = idSpedizione });
            }
        }

        private bool ShippingExists(int id)
        {
            return _context.Shippings.Any(e => e.IdSpedizione == id);
        }
    }
}

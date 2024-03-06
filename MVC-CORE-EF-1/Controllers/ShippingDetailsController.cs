using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CORE_EF_1.Data;
using MVC_CORE_EF_1.Models;

namespace MVC_CORE_EF_1.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class ShippingDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShippingDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShippingDetails.Include(s => s.Shipping);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShippingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Shipping)
                .FirstOrDefaultAsync(m => m.IdShippingDetail == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // GET: ShippingDetails/Create
        public IActionResult Create()
        {
            ViewData["IdSpedizione"] = new SelectList(_context.Shippings, "IdSpedizione", "IndirizzoDestinatario");
            return View();
        }
        public IActionResult CreateStatus(int? idSpedizione)
        {
            TempData["idSpedizione"] = idSpedizione;
            return View();
        }

        // POST: ShippingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSpedizione,StatoSpedizione,LuogoCorrente,NoteSpedizione,DataAggiornamento")] ShippingDetail shippingDetail)
        {
            try
            {
                _context.Add(shippingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ModelState.AddModelError("", ex.Message);
                ViewData["IdSpedizione"] = new SelectList(_context.Shippings, "IdSpedizione", "IndirizzoDestinatario", shippingDetail.IdSpedizione);
                return View(shippingDetail);
            }
        }


        // GET: ShippingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }
            ViewData["IdSpedizione"] = new SelectList(_context.Shippings, "IdSpedizione", "IndirizzoDestinatario", shippingDetail.IdSpedizione);
            return View(shippingDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdShippingDetail,IdSpedizione,StatoSpedizione,LuogoCorrente,NoteSpedizione,DataAggiornamento")] ShippingDetail shippingDetail)
        {
            try
            {
                _context.Update(shippingDetail);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewData["IdSpedizione"] = new SelectList(_context.Shippings, "IdSpedizione", "IndirizzoDestinatario", shippingDetail.IdSpedizione);
                return View(shippingDetail);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ShippingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Shipping)
                .FirstOrDefaultAsync(m => m.IdShippingDetail == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // POST: ShippingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail != null)
            {
                _context.ShippingDetails.Remove(shippingDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingDetailExists(int id)
        {
            return _context.ShippingDetails.Any(e => e.IdShippingDetail == id);
        }
    }
}

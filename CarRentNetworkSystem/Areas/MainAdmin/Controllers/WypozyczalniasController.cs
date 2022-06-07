using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATHCarRentNetworkSystem.Data;
using ATHCarRentNetworkSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace ATHCarRentNetworkSystem.Areas.MainAdmin.Controllers
{
    [Area("MainAdmin")]
    [Authorize(Roles = "Admin")]
    public class WypozyczalniasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WypozyczalniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MainAdmin/Wypozyczalnias
        public async Task<IActionResult> Index()
        {
              return _context.wypozyczalnias != null ? 
                          View(await _context.wypozyczalnias.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.wypozyczalnias'  is null.");
        }

        // GET: MainAdmin/Wypozyczalnias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.wypozyczalnias == null)
            {
                return NotFound();
            }

            var wypozyczalnia = await _context.wypozyczalnias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczalnia == null)
            {
                return NotFound();
            }

            return View(wypozyczalnia);
        }

        // GET: MainAdmin/Wypozyczalnias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainAdmin/Wypozyczalnias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Wypozyczalnia wypozyczalnia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wypozyczalnia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wypozyczalnia);
        }

        // GET: MainAdmin/Wypozyczalnias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.wypozyczalnias == null)
            {
                return NotFound();
            }

            var wypozyczalnia = await _context.wypozyczalnias.FindAsync(id);
            if (wypozyczalnia == null)
            {
                return NotFound();
            }
            return View(wypozyczalnia);
        }

        // POST: MainAdmin/Wypozyczalnias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Wypozyczalnia wypozyczalnia)
        {
            if (id != wypozyczalnia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wypozyczalnia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WypozyczalniaExists(wypozyczalnia.Id))
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
            return View(wypozyczalnia);
        }

        // GET: MainAdmin/Wypozyczalnias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.wypozyczalnias == null)
            {
                return NotFound();
            }

            var wypozyczalnia = await _context.wypozyczalnias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wypozyczalnia == null)
            {
                return NotFound();
            }

            return View(wypozyczalnia);
        }

        // POST: MainAdmin/Wypozyczalnias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.wypozyczalnias == null)
            {
                return Problem("Entity set 'ApplicationDbContext.wypozyczalnias'  is null.");
            }
            var wypozyczalnia = await _context.wypozyczalnias.FindAsync(id);
            if (wypozyczalnia != null)
            {
                _context.wypozyczalnias.Remove(wypozyczalnia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WypozyczalniaExists(int id)
        {
          return (_context.wypozyczalnias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

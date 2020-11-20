using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Session11Stickies.Data;
using Session11Stickies.Models;

namespace Session11Stickies.Controllers
{
    public class StickiesController : Controller
    {
        private readonly StickiesDbContext _context;

        public StickiesController(StickiesDbContext context)
        {
            _context = context;
        }

        // GET: Stickies
        public async Task<IActionResult> Index()
        {
            var stickiesDbContext = _context.Stickies.Include(s => s.Category);
            return View(await stickiesDbContext.ToListAsync());
        }

        // GET: Stickies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sticky = await _context.Stickies
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sticky == null)
            {
                return NotFound();
            }

            return View(sticky);
        }

        // GET: Stickies/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Stickies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Timestamp,SortOrder,CategoryId")] Sticky sticky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sticky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", sticky.CategoryId);
            return View(sticky);
        }

        // GET: Stickies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sticky = await _context.Stickies.FindAsync(id);
            if (sticky == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", sticky.CategoryId);
            return View(sticky);
        }

        // POST: Stickies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Timestamp,SortOrder,CategoryId")] Sticky sticky)
        {
            if (id != sticky.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sticky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StickyExists(sticky.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", sticky.CategoryId);
            return View(sticky);
        }

        // GET: Stickies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sticky = await _context.Stickies
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sticky == null)
            {
                return NotFound();
            }

            return View(sticky);
        }

        // POST: Stickies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sticky = await _context.Stickies.FindAsync(id);
            _context.Stickies.Remove(sticky);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StickyExists(int id)
        {
            return _context.Stickies.Any(e => e.Id == id);
        }
    }
}

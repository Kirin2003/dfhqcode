using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopConferenceProj.Data;
using TopConferenceProj.Models;

namespace TopConferenceProj.Controllers
{
    public class PapersController : Controller
    {
        private readonly TopConferenceProjContext _context;

        public PapersController(TopConferenceProjContext context)
        {
            _context = context;
        }

        // GET: Papers
        public async Task<IActionResult> Index()
        {
              return _context.Paper != null ? 
                          View(await _context.Paper.ToListAsync()) :
                          Problem("Entity set 'TopConferenceProjContext.Paper'  is null.");
        }

        // GET: Papers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paper == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // GET: Papers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Papers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paper);
        }

        // GET: Papers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paper == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper.FindAsync(id);
            if (paper == null)
            {
                return NotFound();
            }
            return View(paper);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Paper paper)
        {
            if (id != paper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaperExists(paper.Id))
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
            return View(paper);
        }

        // GET: Papers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paper == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paper == null)
            {
                return Problem("Entity set 'TopConferenceProjContext.Paper'  is null.");
            }
            var paper = await _context.Paper.FindAsync(id);
            if (paper != null)
            {
                _context.Paper.Remove(paper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaperExists(int id)
        {
          return (_context.Paper?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    public class ProvinciumsController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public ProvinciumsController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Provinciums
        public async Task<IActionResult> Index()
        {
            var trabajadoresPruebaContext = _context.Provincia.Include(p => p.IdDepartamentoNavigation);
            return View(await trabajadoresPruebaContext.ToListAsync());
        }

        // GET: Provinciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // GET: Provinciums/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            return View();
        }

        // POST: Provinciums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdDepartamento,NombreProvincia")] Provincium provincium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincium.IdDepartamento);
            return View(provincium);
        }

        // GET: Provinciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincium.IdDepartamento);
            return View(provincium);
        }

        // POST: Provinciums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdDepartamento,NombreProvincia")] Provincium provincium)
        {
            if (id != provincium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciumExists(provincium.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", provincium.IdDepartamento);
            return View(provincium);
        }

        // GET: Provinciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Provincia == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // POST: Provinciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Provincia == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Provincia'  is null.");
            }
            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium != null)
            {
                _context.Provincia.Remove(provincium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinciumExists(int id)
        {
          return (_context.Provincia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

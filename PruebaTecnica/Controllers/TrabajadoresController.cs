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
    public class TrabajadoresController : Controller
    {
        private readonly TrabajadoresPruebaContext _context;

        public TrabajadoresController(TrabajadoresPruebaContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var trabajadoresPruebaContext = _context.Trabajadores.Include(t => t.IdDepartamentoNavigation).Include(t => t.IdDistritoNavigation).Include(t => t.IdProvinciaNavigation);
            return View(await trabajadoresPruebaContext.ToListAsync());
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["NombreDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["NombreDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            ViewData["NombreProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NombreDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["NombreDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            ViewData["NombreProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }
            ViewData["NombreDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["NombreDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            ViewData["NombreProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoreExists(trabajadore.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "Id", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "Id", trabajadore.IdProvincia);
            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajadores == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trabajadores == null)
            {
                return Problem("Entity set 'TrabajadoresPruebaContext.Trabajadores'  is null.");
            }
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoreExists(int id)
        {
          return (_context.Trabajadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
        public IActionResult ObtenerProvinciasPorDepartamento(int departamentoId)
        {
            // Lógica para obtener las provincias correspondientes al departamento seleccionado
            var provincias = _context.Provincia.Where(p => p.IdDepartamento == departamentoId).Select(p => new { value = p.Id, text = p.NombreProvincia }).ToList();

            return Json(provincias);
        }
        [HttpPost]
        public IActionResult ObtenerDistritosPorProvincia(int provinciaId)
        {
            // Lógica para obtener los distritos correspondientes a la provincia seleccionada
            var distritos = _context.Distritos.Where(d => d.IdProvincia == provinciaId).Select(d => new { value = d.Id, text = d.NombreDistrito }).ToList();

            return Json(distritos);
        }

    }
}

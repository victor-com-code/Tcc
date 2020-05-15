using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Data;
using Tcc_Senai.Models;

namespace Tcc_Senai.Controllers
{
    public class SemestreController : Controller
    {
        private readonly IESContext _context;
        public SemestreController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Semestres.OrderBy(c =>
            c.NomeSemestre).ToListAsync());

        }
        // GET: Semestre Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSemestre", "NomeSemestre", "Ano")] Semestre semestre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(semestre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(semestre);
        }
        // GET: Semestre/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var semestre = await _context.Semestres.SingleOrDefaultAsync(m => m.IdSemestre == id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("IdSemestre", "NomeSemestre", "Ano")] Semestre semestre)
        {
            if (id != semestre.IdSemestre)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestreExists(semestre.IdSemestre))
                    {
                        NotFound();


                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(semestre);
        }
        private bool SemestreExists(long? id)
        {
            return _context.Semestres.Any(e => e.IdSemestre == id);
        }

        // GET: Semestre/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var semestre = await _context.Semestres.SingleOrDefaultAsync(m => m.IdSemestre == id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }
        // POST: Semestre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var semestre = await _context.Semestres.SingleOrDefaultAsync(m => m.IdSemestre == id);
            _context.Semestres.Remove(semestre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
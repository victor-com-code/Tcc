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
    public class UnidadeCurricularController : Controller
    {
        private readonly IESContext _context;
        public UnidadeCurricularController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadeCurriculares.OrderBy(c =>
            c.Nome).ToListAsync());
        }
        // GET: UC Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Nome")] UnidadeCurricular unidadecurricular)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(unidadecurricular);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(unidadecurricular);
        }
        // GET: UC /Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var unidadecurricular = await _context.UnidadeCurriculares.SingleOrDefaultAsync(m => m.Id == id);
            if (unidadecurricular == null)
            {
                return NotFound();
            }
            return View(unidadecurricular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "Nome")] UnidadeCurricular unidadecurricular)
        {
            if (id != unidadecurricular.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadecurricular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeCurricularExists(unidadecurricular.Id))
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
            return View(unidadecurricular);
        }
        private bool UnidadeCurricularExists(long? id)
        {
            return _context.UnidadeCurriculares.Any(e => e.Id == id
            );
        }

        // GET: UC/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var unidadecurricular = await _context.UnidadeCurriculares.SingleOrDefaultAsync(m => m.Id == id);
            if (unidadecurricular == null)
            {
                return NotFound();
            }
            return View(unidadecurricular);
        }
        // POST: UC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var unidadecurricular = await _context.UnidadeCurriculares.SingleOrDefaultAsync(m => m.Id == id);
            _context.UnidadeCurriculares.Remove(unidadecurricular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
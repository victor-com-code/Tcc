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
    public class GerarCursoController : Controller
    {
        private readonly IESContext _context;
        public GerarCursoController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.GerarCursos.OrderBy(c =>
            c.DataInicio).ToListAsync());

        }
        // GET: GerarCurso Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGerarCurso", "DataInicio", "DataFim", "NomeModalidade", "NomeCurso")] GerarCurso gerar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(gerar);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(gerar);
        }
        // GET: Gerar Curso/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gerar = await _context.GerarCursos.SingleOrDefaultAsync(m => m.IdGerarCursos == id);
            if (gerar == null)
            {
                return NotFound();
            }
            return View(gerar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("IdGerarCurso", "DataInicio", "DataFim", "NomeModalidade", "NomeCurso")] GerarCurso gerar)
        {
            if (id != gerar.IdGerarCursos)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gerar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GerarCursoExists(gerar.IdGerarCursos))
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
            return View(gerar);
        }
        private bool GerarCursoExists(long? id)
        {
            return _context.GerarCursos.Any(e => e.IdGerarCursos == id);
        }

        // GET: Gerar Curso/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gerar = await _context.GerarCursos.SingleOrDefaultAsync(m => m.IdGerarCursos == id);
            if (gerar == null)
            {
                return NotFound();
            }
            return View(gerar);
        }
        // POST: Gerar Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var gerar = await _context.GerarCursos.SingleOrDefaultAsync(m => m.IdGerarCursos == id);
            _context.GerarCursos.Remove(gerar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
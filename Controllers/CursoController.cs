using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Data;
using Tcc_Senai.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tcc_Senai.Controllers
{
    public class CursoController : Controller
    {
        private readonly IESContext _context;
        public CursoController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cursos.Include(c =>c.Modalidade).Include(i => i.UnidadeCurricular).OrderBy(b => b.NomeCurso).ToListAsync());
        }
        // GET: Curso Create
        public ActionResult Create()
        {
            var modalidade = _context.Modalidades.OrderBy(i => i.NomeModalidade).ToList();
            modalidade.Insert(0, new Modalidade() { IdModalidade = 0, NomeModalidade = "Selecione a Modalidade" });
            ViewBag.Modalidades = modalidade;
            var unidadeCurriculars = _context.UnidadeCurriculares.OrderBy(i => i.NomeUnidadeCurricular).ToList();
            unidadeCurriculars.Insert(0, new UnidadeCurricular() { IdUc = 0, NomeUnidadeCurricular = "Selecione a Unidade Curricular" });
            ViewBag.UnidadeCurriculares = unidadeCurriculars;
            return View();
        }
        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCurso", "NomeCurso", "NomeModalidade, IdModalidade", "NomeUnidadeCurricular, IdUc", "CargaHoraria", "Sigla")] Curso curso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(curso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(curso);
        }
        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }
            ViewBag.Modalidades = new SelectList(_context.Modalidades.OrderBy(b => b.NomeModalidade),"IdModalidade", "NomeModalidade", curso.IdModalidade);
            ViewBag.UnidadeCurriculares = new SelectList(_context.UnidadeCurriculares.OrderBy(b => b.NomeUnidadeCurricular), "IdUc", "NomeUnidadeCurricular", curso.IdUc);
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("IdCurso", "NomeCurso", "IdModalidade", "IdUc", "CargaHoraria", "Sigla")] Curso curso )
        {
            if (id != curso.IdCurso)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.IdCurso))
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
            ViewBag.Modalidades = new SelectList(_context.Modalidades.OrderBy(i => i.NomeModalidade), "IdModalidade", "NomeModalidade", curso.IdModalidade);
            ViewBag.UnidadeCurriculares = new SelectList(_context.UnidadeCurriculares.OrderBy(c => c.NomeUnidadeCurricular), "IdUc", "NomeUnidadeCurricular", curso.IdUc);
            return View(curso);
        }
        private bool CursoExists(long? id)
        {
            return _context.Cursos.Any(e => e.IdCurso == id);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.IdCurso == id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }
        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.IdCurso == id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    //dshdkjadjsadhjaskddsakdas
    //dshdkjadjsadhjaskddsakdas
    //dshdkjadjsadhjaskddsakdas
    //dshdkjadjsadhjaskddsakdas
    //dshdkjadjsadhjaskddsakdas
    //dshdkjadjsadhjaskddsakdas
}
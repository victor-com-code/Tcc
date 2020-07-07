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
            return View(await _context.Cursos.Include(c => c.Modalidade).OrderBy(b => b.Nome).ToListAsync());
        }

        // GET: Curso Create
        public ActionResult Create()
        {
            var modalidade = _context.Modalidades.OrderBy(i => i.Nome).ToList();
            modalidade.Insert(0, new Modalidade() { Id = 0, Nome = "Selecione a Modalidade" });
            ViewBag.Modalidades = modalidade;

            var unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            unidades.Insert(0, new UnidadeCurricular() { Id = 0, Nome = "Selecione as Unidades Curriculares" });
            ViewBag.Unidades = new MultiSelectList(unidades, "Id", "Nome");
            
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Nome", "IdMod", "CargaHoraria", "Sigla")] Curso curso, int[] UnidadeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!haveCursos(curso)) 
                    {
                        _context.Add(curso);
                        await _context.SaveChangesAsync();

                        var currentCurso = _context.Cursos.Where(c => c.Nome.Equals(curso.Nome)).SingleOrDefault();
                        // Para cada unidade selecionada cria a relação Curso -> Unidades Curriculares
                        foreach (var ids in UnidadeId)
                        {
                            CursoUnidadeCurricular cursoUnidade = new CursoUnidadeCurricular();
                            cursoUnidade.IdCurso = currentCurso.Id;
                            cursoUnidade.IdUc = ids;
                            _context.Add(cursoUnidade);
                            await _context.SaveChangesAsync();
                        }
                        
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["MSG_E"] = "Já existe um Curso cadastrado com esse nome.";
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            var modalidade = _context.Modalidades.OrderBy(i => i.Nome).ToList();
            modalidade.Insert(0, new Modalidade() { Id = 0, Nome = "Selecione a Modalidade" });
            ViewBag.Modalidades = modalidade;

            var unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            unidades.Insert(0, new UnidadeCurricular() { Id = 0, Nome = "Selecione as Unidades Curriculares" });
            ViewBag.Unidades = new MultiSelectList(unidades, "Id", "Nome");
            return View(curso);
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }
            var ucSelecionada = _context.CursoUnidadeCurriculares.Where(c => c.IdCurso == id).ToList();
            List<int> selecionadas = new List<int>();

            foreach (var s in ucSelecionada)
            {
                selecionadas.Add(Convert.ToInt32(s.IdUc));
            }

            ViewBag.Modalidades = new SelectList(_context.Modalidades.OrderBy(b => b.Nome), "Id", "Nome", curso.IdMod);         
            ViewBag.Unidades = new MultiSelectList(_context.UnidadeCurriculares.OrderBy(u => u.Nome), "Id", "Nome", selecionadas);
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "Nome", "IdMod", "CargaHoraria", "Sigla")] Curso curso, int[] UnidadeId)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();

                    deleteCursoUnidade(id);
                    var ucs = _context.CursoUnidadeCurriculares.Where(c => c.IdCurso.Equals(id)).ToList();

                    // Para cada unidade selecionada cria a relação Curso -> Unidades Curriculares
                    foreach (var unidade in UnidadeId)
                    {
                        CursoUnidadeCurricular cursoUnidade = new CursoUnidadeCurricular();
                        cursoUnidade.IdCurso = curso.Id;
                        cursoUnidade.IdUc = unidade;
                        _context.Add(cursoUnidade);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            ViewBag.Modalidades = new SelectList(_context.Modalidades.OrderBy(i => i.Nome), "Id", "Nome", curso.IdMod);
            ViewBag.Unidades = new MultiSelectList(_context.UnidadeCurriculares.OrderBy(u => u.Nome), "Id", "Nome");
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.Id == id);
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
            deleteCursoUnidade(id);
            var curso = await _context.Cursos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Cursos.Include(c => c.Modalidade).OrderBy(b => b.Nome).ToListAsync());
        }

        private bool CursoExists(long? id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }

        public bool haveCursos(Curso curso)
        {
            var have = _context.Cursos.Where(c => c.Nome.Equals(curso.Nome)).SingleOrDefault();
            if (have != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool deleteCursoUnidade(long? id)
        {
            // pegando as Curso -> Unidades curriculares existentes
            var ucs = _context.CursoUnidadeCurriculares.Where(c => c.IdCurso.Equals(id)).ToList();
            foreach (var uc in ucs)
            {
                // excluindo as unidades existentes
                _context.CursoUnidadeCurriculares.Remove(uc);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
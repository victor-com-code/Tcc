

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
    public class ProfessorController : Controller

    {
       
        private readonly IESContext _context;
        public ProfessorController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professores.OrderBy(c =>
            c.NomeCompleto).ToListAsync());
            
        }
        // GET: Professor Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind( "IdProfessor", "NomeCompleto", "PrimeiroNome", "TipoDeContrato", "HorarioDeTrabalho","CargaHorariaSemanal")] Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(professor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(professor);
        }
        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var professor = await _context.Professores.SingleOrDefaultAsync(m => m.IdProfessor == id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("IdProfessor", "NomeCompleto", "PrimeiroNome", "TipoDeContrato", "HorarioDeTrabalho", "CargaHorariaSemanal")] Professor professor)
        {
            if (id != professor.IdProfessor)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.IdProfessor))
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
            return View(professor);
        }
        private bool ProfessorExists(long? id)
        {
            return _context.Professores.Any(e => e.IdProfessor == id
            );
        }
       
        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var professor = await _context.Professores.SingleOrDefaultAsync(m => m.IdProfessor == id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }
        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var professor = await _context.Professores.SingleOrDefaultAsync(m => m.IdProfessor == id);
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}
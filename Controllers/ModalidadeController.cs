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
    public class ModalidadeController : Controller
    {

        private readonly IESContext _context;
        public ModalidadeController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Modalidades.OrderBy(c =>
            c.NomeModalidade).ToListAsync());
        }
        // GET: Modalidade/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeModalidade")] Modalidade modalidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(modalidade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(modalidade);
        }
        // GET: Modalidade/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var modalidade = await _context.Modalidades.SingleOrDefaultAsync(m => m.IdModalidade == id);
            if (modalidade == null)
            {
                return NotFound();
            }
            return View(modalidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("IdModalidade,NomeModalidade")] Modalidade modalidade)
        {
            if (id != modalidade.IdModalidade)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modalidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModalidadeExists(modalidade.IdModalidade))
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
            return View(modalidade);
        }
        private bool ModalidadeExists(long? id)
        {
            return _context.Modalidades.Any(e => e.IdModalidade == id
            );
        }
       
        // GET: Modalidade/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var modalidade = await _context.Modalidades.SingleOrDefaultAsync(m => m.IdModalidade == id);
            if (modalidade == null)
            {
                return NotFound();
            }
            return View(modalidade);
        }
        // POST: Modalidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var modalidade = await _context.Modalidades.SingleOrDefaultAsync(m => m.IdModalidade == id);
            _context.Modalidades.Remove(modalidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}


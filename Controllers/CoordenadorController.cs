using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Data;
using Tcc_Senai.Models;
using System.Web;

namespace Tcc_Senai.Controllers
{
    public class CoordenadorController : Controller
    {
        //private readonly IESContext _context;
        //public CoordenadorController(IESContext context)
        //{
        //    this._context = context;
        //}
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Coordenadores.OrderBy(c =>
        //    c.NomeCompleto).ToListAsync());

        //}
        //// GET: Coordenador Create
        //// blz
        //public ActionResult Create()
        //{
        //    return View();
        //}
        ////POST: Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind( "IdCoordenador", "NomeCompleto",  "NomeDoUsuario", "Email", "Sexo", "TipoDeAcesso", "Senha", "ConfirmarSenha")] Coordenador coordenador)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(coordenador);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DbUpdateException)
        //    {
        //        ModelState.AddModelError("", "Não foi possível inserir os dados.");
        //    }
        //    return View(coordenador);
        //}
        //// GET: Coordenador/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var coordenador = await _context.Coordenadores.SingleOrDefaultAsync(m => m.IdCoordenador == id);
        //    if (coordenador == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(coordenador);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long? id, [Bind("IdCoordenador", "NomeCompleto", "NomeDoUsuario", "Email", "Sexo", "TipoDeAcesso", "Senha", "ConfirmarSenha")] Coordenador coordenador)
        //{
        //    if (id != coordenador.IdCoordenador)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(coordenador);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CoordenadorExists(coordenador.IdCoordenador)) 
        //            {
        //                NotFound();


        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(coordenador);
        //}
        //private bool CoordenadorExists(long? id)
        //{
        //    return _context.Coordenadores.Any(e => e.IdCoordenador == id
        //    );
        //}
        
        //// GET: Coordenador/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var coordenador = await _context.Coordenadores.SingleOrDefaultAsync(m => m.IdCoordenador == id);
        //    if (coordenador == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(coordenador);
        //}
        //// POST: Coordenador/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long? id)
        //{
        //    var coordenador = await _context.Coordenadores.SingleOrDefaultAsync(m => m.IdCoordenador == id);
        //    _context.Coordenadores.Remove(coordenador);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
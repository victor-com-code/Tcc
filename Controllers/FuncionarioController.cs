

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
    public class FuncionarioController : Controller
    { 
        private readonly IESContext _context;
        public FuncionarioController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.OrderBy(c =>
            c.NomeCompleto).ToListAsync());
        }

        // GET: Funcionario Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Funcionario Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email", "Senha")] Curso curso)
        {
            return View();
        }

        // GET: Funcionario Create
        public ActionResult Create()
        {
            var perfis = _context.Perfis.OrderBy(i => i.Nivel).ToList();
            perfis.Insert(0, new Perfil() { Id = 0, Nivel = "Selecione o Perfil de Funcionário" });
            ViewBag.Perfis = perfis;

            var contratos = _context.Contratos.OrderBy(i => i.Tipo).ToList();
            contratos.Insert(0, new Contrato() { Id = 0, Tipo = "Selecione o Tipo de Contrato do Funcionário" });
            ViewBag.Contratos = contratos;
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "NomeCompleto", "Email", "Senha", "ConfirmarSenha", "IdPerfil", "IdContrato", "Horario", "CargaHorariaSemanal")] Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(funcionario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }

            var perfis = _context.Perfis.OrderBy(i => i.Nivel).ToList();
            perfis.Insert(0, new Perfil() { Id = 0, Nivel = "Selecione o Perfil de Funcionário" });
            ViewBag.Perfis = perfis;

            var contratos = _context.Contratos.OrderBy(i => i.Tipo).ToList();
            contratos.Insert(0, new Contrato() { Id = 0, Tipo = "Selecione o Tipo de Contrato do Funcionário" });
            ViewBag.Contratos = contratos;

            return View(funcionario);
        }
        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.Id == id);
            if (Funcionario == null)
            {
                return NotFound();
            }
            ViewBag.Perfis = _context.Perfis.OrderBy(i => i.Nivel).ToList();
            ViewBag.Contratos = _context.Contratos.OrderBy(i => i.Tipo).ToList();
            return View(Funcionario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "NomeCompleto", "Email", "Senha", "ConfirmarSenha", "IdPerfil", "IdContrato", "Horario", "CargaHorariaSemanal")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
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
            return View(funcionario);
        }
        private bool FuncionarioExists(long? id)
        {
            return _context.Funcionarios.Any(e => e.Id == id
            );
        }
       
        // GET: Funcionario/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.Id == id);
            if (Funcionario == null)
            {
                return NotFound();
            }
            return View(Funcionario);
        }
        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var Funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.Id == id);
            _context.Funcionarios.Remove(Funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
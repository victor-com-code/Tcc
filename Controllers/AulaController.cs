using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tcc_Senai.Data;
using Tcc_Senai.Models;

namespace Tcc_Senai.Controllers
{
    public class AulaController : Controller
    {
        private readonly IESContext _context;
        public AulaController(IESContext context)
        {
            this._context = context;
        }

        //GET Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aulas.Include(a => a.Turma).Include(u => u.UnidadeCurricular).Include(f => f.Funcionario).OrderBy(a => a.Turma.Sigla).ToListAsync());
        }

        //GET Create
        public ActionResult Create()
        {
            // trazendo as turmas do banco
            var turmas = _context.Turmas.OrderBy(t => t.Sigla).ToList();
            turmas.Insert(0, new Turma() { Id = 0, Sigla = "Selecione a Turma" });
            ViewBag.Turmas = turmas;

            // trazendo as unidades do banco
            var unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            unidades.Insert(0, new UnidadeCurricular() { Id = 0, Nome = "Selecione a Unidade Curricular" });
            ViewBag.Unidades = unidades;

            // trazendo os funcionarios do banco
            var funcionarios = _context.Funcionarios.OrderBy(f => f.NomeCompleto).ToList();
            funcionarios.Insert(0, new Funcionario() { Id = 0, NomeCompleto = "Selecione o Funcionário" });
            ViewBag.Funcionarios = funcionarios;
         
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "IdTurma", "Data", "HorarioInicio", "HorarioFim", "IdUc", "IdFunc")]Aula aula)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // se não houver aula na data e horário passados, insere a aula no banco
                    if (!haveAulaT(aula, null) && !haveAulaF(aula, null))
                    {
                        // adicionando a aula ao banco
                        _context.Add(aula);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["MSG_ERR"] = "Erro! Já existe uma aula cadastrada nessa data e coincidente com este horário";
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
            ViewBag.Turmas = _context.Turmas.OrderBy(t => t.Sigla).ToList();
            ViewBag.Unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            ViewBag.Funcionarios = _context.Funcionarios.OrderBy(f => f.NomeCompleto).ToList();
            return View();
        }

        //GET Edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            var aula = await _context.Aulas.SingleOrDefaultAsync(a => a.Id == id);
            if (aula == null)
            {
                return NotFound();
            }
            ViewBag.Turmas = _context.Turmas.OrderBy(t => t.Sigla).ToList();
            ViewBag.Unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            ViewBag.Funcionarios = _context.Funcionarios.OrderBy(f => f.NomeCompleto).ToList();
            return View(aula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "IdTurma", "Data", "HorarioInicio", "HorarioFim", "IdUc", "IdFunc")]Aula aula)
        {
            if (id != aula.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (!haveAulaT(aula, id) && !haveAulaF(aula, id))
                {
                    try
                    {
                        _context.Update(aula);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbException)
                    {

                        if (!AulaExists(aula.Id))
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
                else
                {
                    ViewData["MSG_ERR"] = "Erro! Já existe uma aula cadastrada nessa data e coincidente com este horário";
                }  
            }
            ViewBag.Turmas = _context.Turmas.OrderBy(t => t.Sigla).ToList();
            ViewBag.Unidades = _context.UnidadeCurriculares.OrderBy(u => u.Nome).ToList();
            ViewBag.Funcionarios = _context.Funcionarios.OrderBy(f => f.NomeCompleto).ToList();
            return View(aula);
        }

        // GET Delete
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aula = await _context.Aulas.SingleOrDefaultAsync(a => a.Id == id);
            if (aula == null)
            {
                return NotFound();
            }
            return View(aula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var aula = await _context.Aulas.SingleOrDefaultAsync(m => m.Id == id);
            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool AulaExists(long? id)
        {
            return _context.Aulas.Any(e => e.Id == id);
        }

        private bool haveAulaT(Aula newAula, long? id)
        {
            bool tem = false;
            var aulas = _context.Aulas.Where(a => a.Turma.Id.Equals(newAula.IdTurma) && a.Data.Equals(newAula.Data)).AsNoTracking().ToList();
            if (aulas != null)
            {
                foreach (var aula in aulas)
                {
                    // se os horários da turma se coincidirem, retorna verdadeiro
                    if (aula.HorarioInicio.TimeOfDay <= newAula.HorarioFim.TimeOfDay && aula.HorarioFim.TimeOfDay >= newAula.HorarioInicio.TimeOfDay)
                    {
                        // se id enviado for igual ao id da aula vindo do banco e o tem for false (Usado no editar)
                        if (id == aula.Id && tem == false)
                        {
                            continue;
                        }
                        else
                        {
                            tem = true;
                        }
                    }
                }
            }
            return tem;
        }

        private bool haveAulaF(Aula newAula, long? id)
        {
            bool tem = false;
            var aulas = _context.Aulas.Where(a => a.Funcionario.Id.Equals(newAula.IdFunc) && a.Data.Equals(newAula.Data)).AsNoTracking().ToList();
            
            // Se a consulta do funcionario não estiver nula, entra na condição
            if (aulas != null)
            {
                foreach (var aula in aulas)
                {
                    // se os horários do funcionario se coincidirem, retorna verdadeiro
                    if (aula.HorarioInicio.TimeOfDay <= newAula.HorarioFim.TimeOfDay && aula.HorarioFim.TimeOfDay >= newAula.HorarioInicio.TimeOfDay)
                    {
                        // se id enviado for igual ao id da aula vindo do banco e o tem for false (Usado no editar)
                        if (id == aula.Id && tem == false)
                        {
                            continue;
                        }   
                        else
                        {
                            tem = true;
                        }
                    }
                }
            }
            return tem;
        }
    }
}
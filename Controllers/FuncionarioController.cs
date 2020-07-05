

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(await _context.Funcionarios.Include(f => f.Perfil).Include(f => f.Contrato).OrderBy(c =>
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
        public async Task<IActionResult> Login([Bind("Email", "Senha")] Funcionario funcionario)
        {
            try
            {
                var user = _context.Funcionarios.Where(u => u.Email.Equals(funcionario.Email)).FirstOrDefault();
                if (user != null)
                {
                    if (user.Senha.Equals(funcionario.Senha))
                    {
                        //HttpContext.Session.Set("SessionId", new byte[] { Convert.ToByte(user.Id) });
                        //HttpContext.Session.SetString("Funcionario", user.NomeCompleto);
                        //HttpContext.Session.SetString("Perfil", Convert.ToString(user.IdPerfil));
                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewData["MSG_ER"] = "E-mail ou Senha Incorretos! Tente Novamente.";
            }
            catch (Exception)
            {
                throw;
            }
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

            var cursos = _context.Cursos.OrderBy(c => c.Nome).ToList();
            cursos.Insert(0, new Curso() { Id = 0, Nome = "Selecione os Cursos que esse Funcionário está relacionado" });
            ViewBag.Cursos = new MultiSelectList(cursos, "Id", "Nome"); 
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "NomeCompleto", "Email", "Senha", "ConfirmarSenha", "IdPerfil", "IdContrato", "Horario", "CargaHorariaSemanal")] Funcionario funcionario, int[] Idcursos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!haveFuncionario(funcionario))
                    {
                        _context.Add(funcionario);
                        await _context.SaveChangesAsync();

                        var currentFuncionario = _context.Funcionarios.Where(f => f.Email.Equals(funcionario.Email)).SingleOrDefault();

                        foreach (var curso in Idcursos)
                        {
                            FuncionarioCurso funcionarioCurso = new FuncionarioCurso();
                            funcionarioCurso.IdCurso = curso;
                            funcionarioCurso.IdFunc = currentFuncionario.Id;
                            _context.Add(funcionarioCurso);
                            await _context.SaveChangesAsync();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["MSG_E"] = "Já existe um Funcionario cadastrado com esse e-mail.";
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

            var cursos = _context.Cursos.OrderBy(c => c.Nome).ToList();
            cursos.Insert(0, new Curso() { Id = 0, Nome = "Selecione os Cursos que esse Funcionário está relacionado" });
            ViewBag.Cursos = new MultiSelectList(cursos, "Id", "Nome");

            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewBag.Perfis = new SelectList(_context.Perfis.OrderBy(i => i.Nivel), "Id", "Nivel", funcionario.IdPerfil);
            ViewBag.Contratos = new SelectList(_context.Contratos.OrderBy(i => i.Tipo), "Id", "Tipo", funcionario.IdContrato);
            ViewBag.Cursos = new MultiSelectList(_context.Cursos.OrderBy(c => c.Nome), "Id", "Nome");
            return View(funcionario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "NomeCompleto", "Email", "Senha", "ConfirmarSenha", "IdPerfil", "IdContrato", "Horario", "CargaHorariaSemanal")] Funcionario funcionario, int[] Idcursos)
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

                    // Deletando os registros ligados a esse funcionario na tabela FuncionarioCurso, é necessário fazer essa
                    // exclusão por não haver possibilidade de alterar registros de uma tabela associativa 
                    deleteFuncionarioCurso(funcionario.Id);

                    var currentFuncionario = _context.Funcionarios.Where(f => f.Id.Equals(id)).SingleOrDefault();

                    foreach (var curso in Idcursos)
                    {
                        FuncionarioCurso funcionarioCurso = new FuncionarioCurso();
                        funcionarioCurso.IdCurso = curso;
                        funcionarioCurso.IdFunc = currentFuncionario.Id;
                        _context.Add(funcionarioCurso);
                        await _context.SaveChangesAsync();
                    }

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
            ViewBag.Perfis = new SelectList(_context.Perfis.OrderBy(i => i.Nivel), "Id", "Nivel", funcionario.IdPerfil);
            ViewBag.Contratos = new SelectList(_context.Contratos.OrderBy(i => i.Tipo), "Id", "Tipo", funcionario.IdContrato);
            ViewBag.Cursos = new MultiSelectList(_context.Cursos.OrderBy(c => c.Nome), "Id", "Nome");
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
            // Deletando os registros ligados a esse funcionario na tabela FuncionarioCurso
            deleteFuncionarioCurso(id);

            var Funcionario = await _context.Funcionarios.SingleOrDefaultAsync(m => m.Id == id);
            _context.Funcionarios.Remove(Funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET List, view semelhante a Index, porém sem opções de DML
        public async Task<IActionResult> List()
        {
            return View(await _context.Funcionarios.Include(f => f.Perfil).Include(f => f.Contrato).OrderBy(c =>
            c.NomeCompleto).ToListAsync());
        }

        // GET Cronograma
        public IActionResult Cronograma(long? id)
        {
            // trazendo do banco todas as aulas do funcionario selecionado
            var aulas = _context.Aulas.Include(a => a.Turma).Include(u => u.UnidadeCurricular).Include(f => f.Funcionario).Where(f => f.IdFunc.Equals(id)).OrderBy(a => a.Data).ToList();
            var funcionario = _context.Funcionarios.SingleOrDefault(t => t.Id.Equals(id));
            ViewBag.Nome = funcionario.NomeCompleto;
            return View(aulas);
        }

        public bool haveFuncionario(Funcionario funcionario)
        {
            var have = _context.Funcionarios.Where(f => f.Email.Equals(funcionario.Email)).SingleOrDefault();
            if (have != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteFuncionarioCurso(long? id)
        {
            var fcs = _context.FuncionarioCursos.Where(f => f.IdFunc.Equals(id)).ToList();
            foreach (var fc in fcs)
            {
                _context.FuncionarioCursos.Remove(fc);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
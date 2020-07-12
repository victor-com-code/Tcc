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
            return View(await _context.Aulas.Include(a => a.Turma).Include(u => u.UnidadeCurricular).Include(f => f.Funcionario).OrderBy(a => a.Data).ToListAsync());
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
                    // se o usuário tiver contrato intermitente e
                    // se a data da aula não coincidir com a folga do funcionário, entra no if
                    if (isIntermitente(aula) && dayLimit(aula))
                    {
                        ViewData["MSG_ERR"] = "Erro! A data selecionada coincide com a folga desse Funcionário!";
                    }
                    else
                    {
                        // se não houver aula na data e horário passados, insere a aula no banco
                        if (!haveAulaT(aula, null) && !haveAulaF(aula, null))
                        {
                            // se o funcionario não ultrapassar as 10 horas de trabalho
                            if (calculaHora(aula))
                            {
                                // adicionando a aula ao banco
                                _context.Add(aula);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                ViewData["MSG_ERR"] = "Erro! O Funcionário já ultrapassou o limite de horas trabalhadas no dia selecionado!";
                            }
                        }
                        else
                        {
                            ViewData["MSG_ERR"] = "Erro! Já existe uma aula com essa turma ou com esse funcionário cadastrada nessa data e coincidente com este horário";
                        }

                    }
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
                // se o usuário tiver contrato intermitente e
                // se a data da aula não coincidir com a folga do funcionário, entra no if
                if (isIntermitente(aula) && dayLimit(aula))
                {
                    ViewData["MSG_ERR"] = "Erro! A data selecionada coincide com a folga desse Funcionário!";
                }
                else
                {
                    // se não houver aula no mesmo dia e horário, entra no if
                    if (!haveAulaT(aula, id) && !haveAulaF(aula, id))
                    {
                        // se o professor não ultrapassar o limite de horas trabalhadas por dia, entra no if
                        if (calculaHora(aula))
                        {
                            try
                            {
                                // atualizando o banco
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
                            ViewData["MSG_ERR"] = "Erro! O Funcionário já ultrapassou o limite de horas trabalhadas no dia selecionado!";
                        }
                    }
                    else
                    {
                        ViewData["MSG_ERR"] = "Erro! Já existe uma aula com essa turma ou com esse funcionário cadastrada nessa data e coincidente com este horário";
                    }
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

        // retorna true se o funcionario tem aula
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

        // retorna true se a turma tem aula
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

        // calcula a quantidade de horas trabalhadas
        private bool calculaHora(Aula newAula)
        {
            // buscando do banco a aula no dia e com o funcionário inserido
            var aulas = _context.Aulas.Where(a => a.Data.Equals(newAula.Data) && a.Funcionario.Id.Equals(newAula.IdFunc)).AsNoTracking().ToList();
            
            // quantidade de horas trabalhadas
            var conthoras = new TimeSpan();
            var limiteHoras = new TimeSpan(10, 00, 00);

            if(aulas != null)
            {
                // horas da aula inserida
                var horaAula = newAula.HorarioFim.Subtract(newAula.HorarioInicio);

                foreach (var aula in aulas)
                {
                    conthoras += aula.HorarioFim.Subtract(aula.HorarioInicio);
                }
                // conforme Regra de Negócio, se ultrapassar as 10 horas de trabalho diaria, retorna false
                if (conthoras.Add(horaAula) > limiteHoras)
                {
                    return false;
                }
            }
            
            return true;
        }

        // confere no banco se o funcionario tem o contrato intermitente
        private bool isIntermitente (Aula aula)
        {
            var funcionario = _context.Funcionarios.Where(f => f.Id.Equals(aula.IdFunc) && f.Contrato.Tipo.Equals("Intermitente")).SingleOrDefault();
            if (funcionario != null)
            {
                return true;
            }            
            return false;
        }

        // Para calcular a folga do Funcionario
        private DateTime folgaFunc(Aula aula)
        {
            // retorna a última aula do funcionario
            var lastAula = _context.Aulas.Where(a => a.IdFunc.Equals(aula.IdFunc)).OrderByDescending(a => a.Data).AsNoTracking().FirstOrDefault();

            // calcula a folga (7 dias, considerando final de semana)
            var folga = lastAula.Data + new TimeSpan(7, 00, 00, 00);
            return folga;
        }

        // retorna true se o funcionario necessita de folga
        private bool dayLimit (Aula aula)
        {
            // pega a quantidade de dias diferentes trabalhado pelo funcionario
            var days = _context.Aulas.Where(a => a.IdFunc.Equals(aula.IdFunc)).GroupBy(a => a.Data).Select(e => new { e.Key, Count = e.Count() }).ToDictionary(e => e.Key, e => e.Count).ToList();
            
           // conforme regra de negócio, a cada 90 dias é necessário uma folga de 5 dias para o funcionário intermitente
            if (days.Count % 90 == 0)
            {
                // se a data da aula inserida for maior ou igual a data de folga, return false
                if(aula.Data >= folgaFunc(aula))
                {
                    return false;
                }
                return true;
            }
            
            return false;
        }
    }
}
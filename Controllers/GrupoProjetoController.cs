﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class GrupoProjetoController : Controller
    {
        private readonly SupermarketDbContext _context;

        public GrupoProjetoController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: GrupoProjetoes
        public async Task<IActionResult> Index()
        {
              return _context.GrupoProjeto != null ? 
                          View(await _context.GrupoProjeto.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.GrupoProjeto'  is null.");
        }

        // GET: GrupoProjetoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GrupoProjeto == null)
            {
                return NotFound();
            }
            

            var grupoProjeto = await _context.GrupoProjeto
                .FirstOrDefaultAsync(m => m.ProjetoId == id);
            if (grupoProjeto == null)
            {
                return NotFound();
            }
            var funcionarios = _context.Employee.Where(F => F.ProjetoId == id).Include(E => E.Funcao);
            List<Employee> employee = new List<Employee>();
            foreach( var funcionario in funcionarios)
            {
                if (grupoProjeto.Employees == null)
                {
                    grupoProjeto.Employees = employee;
                }
                grupoProjeto.Funcoes!.Add(funcionario.Funcao!);
                grupoProjeto.Employees!.Add(funcionario);
            }

            return View(grupoProjeto);
        }

        // GET: GrupoProjetoes/Create
        public IActionResult Create()
        {

            ViewData["Funcoes"] = new SelectList(_context.Set<Funcao>().Where(F => _context.Employee.Where(e => e.Funcao_FK == F.FuncaoId).Any()), "FuncaoId", "NomeFuncao");
            return View();
        }

        // POST: GrupoProjetoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjetoId,NomeProjeto,DescricaoProjeto,Objectives")] GrupoProjeto grupoProjeto, int[] funcoesId)
        {
            List<Employee> employee = new List<Employee>();
            if (ModelState.IsValid)
            {
                grupoProjeto.Funcoes = _context.Funcao.Where(f => funcoesId.Contains(f.FuncaoId)).ToList();
                _context.Add(grupoProjeto);
                await _context.SaveChangesAsync();
                foreach (var item in funcoesId)
                {
                    employee.Add(GetBestEmployeeFunction(item));
                }
                foreach (var e in employee)
                {
                    e.ProjetoId = grupoProjeto.ProjetoId;
                    _context.Update(e);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();
            return View(grupoProjeto);
        }

        // GET: GrupoProjetoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GrupoProjeto == null)
            {
                return NotFound();
            }

            var grupoProjeto = await _context.GrupoProjeto.FindAsync(id);
            if (grupoProjeto == null)
            {
                return NotFound();
            }
            return View(grupoProjeto);
        }

        // POST: GrupoProjetoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,NomeProjeto,DescricaoProjeto,Objectives")] GrupoProjeto grupoProjeto)
        {
            if (id != grupoProjeto.ProjetoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupoProjeto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoProjetoExists(grupoProjeto.ProjetoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grupoProjeto);
        }

        // GET: GrupoProjetoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GrupoProjeto == null)
            {
                return NotFound();
            }

            var grupoProjeto = await _context.GrupoProjeto
                .FirstOrDefaultAsync(m => m.ProjetoId == id);
            if (grupoProjeto == null)
            {
                return NotFound();
            }

            return View(grupoProjeto);
        }

        // POST: GrupoProjetoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GrupoProjeto == null)
            {
                return Problem("Entity set 'SupermarketDbContext.GrupoProjeto'  is null.");
            }
            var grupoProjeto = await _context.GrupoProjeto.FindAsync(id);
            if (grupoProjeto != null)
            {
                _context.GrupoProjeto.Remove(grupoProjeto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoProjetoExists(int id)
        {
          return (_context.GrupoProjeto?.Any(e => e.ProjetoId == id)).GetValueOrDefault();
        }

        private Employee GetBestEmployeeFunction(int funcaoId)
        {
            if (funcaoId == 0) throw new Exception("Id funcao nao existe");

            var funcionariosF = _context.Employee.Where(E => E.Funcao_FK == funcaoId && E.ProjetoId == 0);
            if (!funcionariosF.Any()) throw new Exception("Lista vazia");
            Employee melhorFuncionario = funcionariosF.First();
            foreach ( var funcao in funcionariosF)
            {
                if (EmployeeGrade(funcao.Funcao_FK) > EmployeeGrade(melhorFuncionario.Funcao_FK)) {
                    melhorFuncionario = funcao;
                }
            }
            return melhorFuncionario;
        }
        private float EmployeeGrade(int? EmployeeId)
        {
            var assiduidade = 1.0;
            if (EmployeeId == null || _context.Employee == null)
            {
                //The employee doesn't exist!
                return 0;
            }

            var Employee = _context.Employee.Find(EmployeeId);
            if (Employee == null)
            {
                //The employee doesn't exist!
                return 0;
            }

            var Evaluations = _context.EmployeeEvaluation.Where(af => af.EmployeeId == Employee.EmployeeId).ToList();
            if (Evaluations.Count < 1)
            {
                //There are no evaluations for this employee
                return 0;
            }
            else
            {
                //TO-DO calcular assiduidade
                var sum = 0;
                foreach (var evaluation in Evaluations)
                {
                    sum += evaluation.GradeNumber;
                }

                var mean = sum / Evaluations.Count;

                mean = (int)(mean * assiduidade);
                return mean;
            }

        }
    }
}

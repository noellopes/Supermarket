﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SupermarketDbContext _context;

        public DepartmentsController(SupermarketDbContext context)
        {
            _context = context;
        }
        public ActionResult CalcularTempoMedioAtendimento(int departamentoId)
        { 
            List<Tickets> tickets = TicketsBaseDados (departamentoId);

            if (tickets.Any())
            {
                TimeSpan tempoMedio = CalcularTempoMedioAtendimento(tickets);
                ViewBag.TempoMedio = tempoMedio.TotalMinutes;
            }
            else
            {
                ViewBag.TempoMedio = 0; 
            }

            return View();
        }

        // Método para obter os tickets associados a um departamento
        private List<Tickets> TicketsBaseDados(int departamentoId)
        {
            using (var dbContext = new SupermarketDbContext(new DbContextOptions<SupermarketDbContext>()))
            {
                List<Tickets> tickets = dbContext.Tickets
                    .Where(t => t.IDDepartments == departamentoId)
                    .OrderBy(t => t.DataEmissao)
                    .ToList();

                return tickets;
            }
        }

        // Método para calcular o tempo médio de atendimento
        private TimeSpan CalcularTempoMedioAtendimento(List<Tickets> tickets)
        {
            List<TimeSpan> temposAtendimento = new List<TimeSpan>();

            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseSqlServer("SupermarketDbContext") 
                .Options;

            using (var dbContext = new SupermarketDbContext(options))
            {
                for (int i = 1; i < tickets.Count; i++)
                {
                    TimeSpan tempoAtendimento = tickets[i].DataAtendimento - tickets[i - 1].DataAtendimento;
                    temposAtendimento.Add(tempoAtendimento);
                }
            }

            TimeSpan tempoMedio = TimeSpan.FromMinutes(temposAtendimento.Average(t => t.TotalMinutes));
            return tempoMedio;
        }

        //pesquisa por nome do departamento 
        public IActionResult pesqNomeTrue(string searchTerm)
    {
        var results = _context.Departments
        .Where(d => (d.StateDepartments.Equals(true)) && d.NameDepartments.Contains(searchTerm))
        .ToList();

            if (results.Count == 0)
        {
          ViewBag.Message = "Nenhum resultado encontrado para a pesquisa.";
        }

        return View("Index", results);
    }
        //pesquisa por nome do departamentoInop 
        public IActionResult pesqNomeFalse(string searchTerm)
        {
            var results = _context.Departments
                .Where(d => (d.StateDepartments.Equals(false)) && d.NameDepartments.Contains(searchTerm))
                .ToList();
            if (results.Count == 0)
            {
                ViewBag.Message = "Nenhum resultado encontrado para a pesquisa.";
            }

            return View("DepInop", results);
        }

        // GET: Departments
        public IActionResult Index(string searchTerm, int page = 1, int pageSize = 2)
        {

            IQueryable<Departments> departmentsQuery = _context.Departments;
            //numero de paginas que da para seelecionar
            var pageSizes = new List<int> { 2, 8, 12, 16, int.MaxValue };

            // Filtra apenas os departamentos ativos
            departmentsQuery = departmentsQuery
                .Where(d => d.StateDepartments.Equals(true));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Aplica a pesquisa se o termo de pesquisa não estiver vazio
                departmentsQuery = departmentsQuery
                    .Where(d => d.NameDepartments.Contains(searchTerm));
            }
            if (!pageSizes.Contains(pageSize))
            {
                pageSizes.Add(pageSize);
            }
            var totalItems = departmentsQuery.Count();

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            var departments = departmentsQuery
                .OrderBy(p => p.NameDepartments)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ProductsListViewModel
            {
                Departments = departments,
                Pagination = pagination,
                SelectedPageSize = pageSize
            };

            // Passa o termo de pesquisa para a view, se houver
            ViewData["SearchTerm"] = searchTerm;
            ViewData["PageSizes"] = new SelectList(pageSizes);
            ViewData["SelectedPageSize"] = pageSize;

            return View(viewModel);
        }
        // GET: DepartmentsInop
        public IActionResult IndexInop(string searchTerm, int page = 1, int pageSize = 2)
        {

            IQueryable<Departments> departmentsQuery = _context.Departments;
            //numero de paginas que da para seelecionar
            var pageSizes = new List<int> { 2, 8, 12, 16, int.MaxValue };

            // Filtra apenas os departamentos ativos
            departmentsQuery = departmentsQuery
                .Where(d => d.StateDepartments.Equals(false));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Aplica a pesquisa se o termo de pesquisa não estiver vazio
                departmentsQuery = departmentsQuery
                    .Where(d => d.NameDepartments.Contains(searchTerm));
            }
            if (!pageSizes.Contains(pageSize))
            {
                pageSizes.Add(pageSize);
            }
            var totalItems = departmentsQuery.Count();

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            var departments = departmentsQuery
                .OrderBy(p => p.NameDepartments)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ProductsListViewModel
            {
                Departments = departments,
                Pagination = pagination,
                SelectedPageSize = pageSize
            };

            // Passa o termo de pesquisa para a view, se houver
            ViewData["SearchTerm"] = searchTerm;
            ViewData["PageSizes"] = new SelectList(pageSizes);
            ViewData["SelectedPageSize"] = pageSize;

            return View("DepInop", viewModel);
        }

        // GET: Departments/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .FirstOrDefaultAsync(m => m.IDDepartments == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                bool DepartmentsExists = await _context.Departments.AnyAsync(
                d => d.NameDepartments == departments.NameDepartments);
                if (DepartmentsExists) {
                    ModelState.AddModelError("", "Another Departments with the same Name already exists.");
                }
                else
                {
                    _context.Add(departments);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Departamento successfully Create.";
                    //book.Author = await _context.Author.FindAsync(book.AuthorId);

                    return View("Details", departments);
                }
                return View(departments);
            }
            //ViewData["IDDepartments"] = new SelectList(_context.Set<Schedule>(), "ScheduleId ", "StateSchedule", Schedule.ScheduleId);
            return View(departments);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Departments departments)
        {
            if (id != departments.IDDepartments)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool boolExists = await _context.Departments.AnyAsync(
                    d => d.NameDepartments == departments.NameDepartments &&d.IDDepartments != departments.IDDepartments);
                    if (boolExists)
                    {
                        ModelState.AddModelError("", "Another Department with same Name Department already exist");
                    }
                    else
                    {
                        _context.Update(departments);
                        await _context.SaveChangesAsync();
                        ViewBag.Message = "Department sucessfully edit.";
                        return View("Details", departments);
                    }
    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.IDDepartments))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return View(departments);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .FirstOrDefaultAsync(m => m.IDDepartments == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Departments'  is null.");
            }
            var departments = await _context.Departments.FindAsync(id);
            if (departments != null)
            {
                _context.Departments.Remove(departments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
          return (_context.Departments?.Any(e => e.IDDepartments == id)).GetValueOrDefault();
        }
    }
}

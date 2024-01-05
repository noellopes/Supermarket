using System;
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

        // GET: Departments
        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 2)
        {
            IQueryable<Department> departmentsQuery = _context.Departments;
            
            var pageSizes = new List<int> { 2, 8, 12, 16, int.MaxValue };

            if (!string.IsNullOrEmpty(searchTerm))
            {
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
                
            var viewModel = new DepListViewModel
            {
                Departments = departments,
                Pagination = pagination,
                SelectedPageSize = pageSize,
                TimeDifferences = new List<TimeSpan>()
            };
            // Aqui você pode percorrer os departamentos e calcular a diferença de tempo para cada ticket
            foreach (var department in departments)
            {
                // Procura o primeiro ticket associado ao departamento
                var firstTicket = _context.Tickets.FirstOrDefault(t => t.IDDepartments == department.IDDepartments);
                // Verifica se existe um ticket associado e se a data de atendimento tem valor
                if (firstTicket != null && firstTicket.DataAtendimento.HasValue)
                {
                    // Obtém as datas de início (DataAtendimento) e fim (DataEmissao)
                    DateTime dataInicio = firstTicket.DataAtendimento.Value;
                    DateTime dataFim = firstTicket.DataEmissao;
                    // Calcula a diferença de tempo entre as duas datas
                    TimeSpan diferenca = dataInicio- dataFim;
                    // Adiciona a diferença de tempo à lista de diferenças de tempo no viewModel
                    viewModel.TimeDifferences.Add(diferenca);
                }
                else
                {
                    // Se não existir ticket ou se a data de atendimento não tiver valor, adiciona TimeSpan.Zero à lista
                    viewModel.TimeDifferences.Add(TimeSpan.Zero);
                }
            }

            ViewData["SearchTerm"] = searchTerm;
            ViewData["PageSizes"] = new SelectList(pageSizes);
            ViewData["SelectedPageSize"] = pageSize;

            return View(viewModel);
        }

        // GET: DepartmentsInop
        public IActionResult IndexInop(string searchTerm, int page = 1, int pageSize = 2)
        {

            IQueryable<Department> departmentsQuery = _context.Departments;
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

            var viewModel = new DepListViewModel
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
        public async Task<IActionResult> Create([Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Department departments)
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
        public async Task<IActionResult> Edit(int id, [Bind("IDDepartments,NameDepartments,DescriptionDepartments,StateDepartments,SkillsDepartments,QuatDepMed")] Department departments)
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

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
       // [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Index(string searchTerm, int page = 1, int pageSize = 2)
        {
            IQueryable<Department> departmentsQuery = _context.Departments;
            
            var pageSizes = new List<int> { 2, 8, 12, 16, int.MaxValue };
            // Filtra apenas os departamentos ativos
            departmentsQuery = departmentsQuery
                .Where(d => d.StateDepartments.Equals(true));
                
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
                TimeDifferences = new List<TimeSpan>(),
                AverageTimes = new List<DepAverageTimeViewModel>(),
                
            };

            foreach (var department in departments)
            {
                // Procura o último ticket associado ao departamento
                var departmentTickets = _context.Tickets
                    .Where(t => t.IDDepartments == department.IDDepartments)
                    .OrderByDescending(t => t.DataAtendimento)
                    .ToList();

                // Obtém o último ticket válido
                var lastValidTicket = departmentTickets.LastOrDefault(t => t.DataAtendimento.HasValue);

                // Calcula a diferença de tempo e adiciona à lista no viewModel
                viewModel.TimeDifferences.Add(CalculateTimeDifference(lastValidTicket, departmentTickets));
                // Calcula a média e adiciona à lista no viewModel
                var averageTime = CalculateAverageTime(departmentTickets.Select(t => CalculateTimeDifference(t, departmentTickets)).ToList());

                viewModel.AverageTimes.Add(new DepAverageTimeViewModel
                {
                    DepartmentId = department.IDDepartments,
                    AverageTime = averageTime
                });
            }

            ViewData["SearchTerm"] = searchTerm;
            ViewData["PageSizes"] = new SelectList(pageSizes);
            ViewData["SelectedPageSize"] = pageSize;

            return View(viewModel);
        }
        private TimeSpan CalculateAverageTime(List<TimeSpan> timeDifferences)
        {
            if (timeDifferences.Any())
            {
                double averageTicks = timeDifferences.Average(t => t.Ticks);
                return TimeSpan.FromTicks((long)averageTicks);
            }
            else
            {
                return TimeSpan.Zero;
            }
        }
        // Função para calcular a média das diferenças de tempo por departamento
        private List<int> CalculateAverageNumberOfTickets(List<Ticket> departmentTickets)
        {
            var averages = new List<int>();

                foreach (var departmentTicket in departmentTickets)
                {
                // Considera todos os tickets com DataAtendimento válida
                var validTickets = departmentTickets
                    .Where(t => t.DataAtendimento.HasValue && t.IDDepartments == departmentTicket.IDDepartments)
                    //.Take(departmentTicket.QuatDepMed)
                    .ToList();

                int average = validTickets.Any() ? (int)Math.Ceiling(validTickets.Average(t => t.NumeroDaSenha)) : 0;
                averages.Add(average);
            }

            return averages;
        }

        private TimeSpan CalculateTimeDifference(Ticket lastValidTicket, List<Ticket> departmentTickets)
        {
            if (lastValidTicket != null)
            {
                DateTime dataInicio = lastValidTicket.DataEmissao;
                DateTime dataFim = lastValidTicket.DataAtendimento ?? GetPreviousValidDate(lastValidTicket, departmentTickets);
                return dataFim - dataInicio;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }

        // Função para obter a data de atendimento do ticket anterior válido
        private DateTime GetPreviousValidDate(Ticket currentTicket, List<Ticket> departmentTickets)
        {
            int currentIndex = departmentTickets.IndexOf(currentTicket);

            for (int i = currentIndex + 1; i < departmentTickets.Count; i++)
            {
                if (departmentTickets[i].DataAtendimento.HasValue)
                {
                    return departmentTickets[i].DataAtendimento.Value;
                }
            }

            // Se nenhum ticket anterior válido for encontrado, retorna DateTime.Now
            return DateTime.Now;
        }
    
    //[Authorize(Roles = "Gestor")]
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
        //[Authorize(Roles = "Gestor")]
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
        //[Authorize(Roles = "Gestor")]
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
        //[Authorize(Roles = "Gestor")]
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
        [Authorize(Roles = "Gestor")]
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
        //[Authorize(Roles = "Gestor")]
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

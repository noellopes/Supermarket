using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class IssuesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public IssuesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index(int page = 1, string product = "", string issuetype = "", string supplier = "", string employee = "")
        {
            var supermarketDbContext = _context.Issues.Include(i => i.Employee).Include(i => i.IssueType).Include(i => i.Product).Include(i => i.Supplier);

            var issues = from i in _context.Issues.Include(p => p.Product)
                                                  .Include(it => it.IssueType)
                                                  .Include(s => s.Supplier)
                                                  .Include(e => e.Employee) select i;
            if(product != "")
            {
                issues = issues.Where(x => x.Product!.Name.Contains(product));
            }

            if (issuetype != "")
            {
                issues = issues.Where(x => x.IssueType!.Name.Contains(issuetype));
            }

            if (supplier != "")
            {
                issues = issues.Where(x => x.Supplier!.Name.Contains(supplier));
            }

            if (employee != "")
            {
                issues = issues.Where(x => x.Employee!.Employee_Name.Contains(employee));
            }

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = issues.Count()
            };

            return View(
                new IssuesListViewModel
                {
                    Issues = issues.OrderByDescending(i => i.IssueRegisterDate)
                                                 .Skip((page - 1) * pagination.PageSize)
                                                 .Take(pagination.PageSize),
                    Pagination = pagination, 
                    SearchProduct = product, 
                    SearchIssueType = issuetype,
                    SearchEmployee = employee,
                    SearchSupplier = supplier
                }
            );
            //return View(await supermarketDbContext.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issues = await _context.Issues
                .Include(i => i.Employee)
                .Include(i => i.IssueType)
                .Include(i => i.Product)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.IssueId == id);
            if (issues == null)
            {
                return View("IssueDeleted");
            }

            return View(issues);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name");
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "Name");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name");
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueId,ProductId,IssueTypeId,Description,SupplierId,EmployeeId,IssueRegisterDate,Severity")] Issues issues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "Name", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name", issues.SupplierId);
            return View(issues);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issues = await _context.Issues.FindAsync(id);
            if (issues == null)
            {
                return View("IssueDeleted");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "Name", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name", issues.SupplierId);
            return View(issues);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueId,ProductId,IssueTypeId,Description,SupplierId,EmployeeId,IssueRegisterDate,Severity")] Issues issues)
        {
            if (id != issues.IssueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuesExists(issues.IssueId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Name", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "Name", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "Name", issues.SupplierId);
            return View(issues);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }

            var issues = await _context.Issues
                .Include(i => i.Employee)
                .Include(i => i.IssueType)
                .Include(i => i.Product)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.IssueId == id);
            if (issues == null)
            {
                return View("IssueDeleted");
            }

            return View(issues);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Issues == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Issues'  is null.");
            }
            var issues = await _context.Issues.FindAsync(id);
            if (issues != null)
            {
                _context.Issues.Remove(issues);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return View("DeleteCompleted", issues);
        }

        private bool IssuesExists(int id)
        {
          return (_context.Issues?.Any(e => e.IssueId == id)).GetValueOrDefault();
        }
    }
}

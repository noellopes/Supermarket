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
        public async Task<IActionResult> Index()
        {
            var supermarketDbContext = _context.Issues.Include(i => i.Employee).Include(i => i.IssueType).Include(i => i.Product).Include(i => i.Supplier);
            return View(await supermarketDbContext.ToListAsync());
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
                return NotFound();
            }

            return View(issues);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Address");
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "IssueDescription");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "SupplierId");
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
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Address", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "IssueDescription", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "SupplierId", issues.SupplierId);
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
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Address", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "IssueDescription", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "SupplierId", issues.SupplierId);
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
            ViewData["EmployeeId"] = new SelectList(_context.Funcionarios, "EmployeeId", "Employee_Address", issues.EmployeeId);
            ViewData["IssueTypeId"] = new SelectList(_context.IssueType, "IssueTypeId", "IssueDescription", issues.IssueTypeId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description", issues.ProductId);
            ViewData["SupplierId"] = new SelectList(_context.Set<Supplier>(), "SupplierId", "SupplierId", issues.SupplierId);
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
                return NotFound();
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
            return RedirectToAction(nameof(Index));
        }

        private bool IssuesExists(int id)
        {
          return (_context.Issues?.Any(e => e.IssueId == id)).GetValueOrDefault();
        }
    }
}

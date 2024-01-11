using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class IssueTypesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public IssueTypesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: IssueTypes
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Index(int page = 1, string issuetype = "")
        {
            var issues_type = from i in _context.IssueType select i;

            if (issuetype != "")
            {
                issues_type = issues_type.Where(x => x!.Name.Contains(issuetype));                
            }

            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = issues_type.Count()
            };

            return View(
                new IssuesTypeListViewModel
                {
                    IssueType = issues_type.OrderByDescending(i => i.Name)
                                                 .Skip((page - 1) * pagination.PageSize)
                                                 .Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchIssueType = issuetype                 
                }
            );
        }

        // GET: IssueTypes/Details/5
        [Authorize(Roles = "View_Reports")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .FirstOrDefaultAsync(m => m.IssueTypeId == id);
            if (issueType == null)
            {
                return View("IssueTypeDeleted");
            }

            return View(issueType);
        }

        // GET: IssueTypes/Create
        [Authorize(Roles = "Create_Edit_Del_IssueType")]        
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create_Edit_Del_IssueType")]
        public async Task<IActionResult> Create([Bind("IssueTypeId,Name,IssueDescription")] IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueType);
        }

        // GET: IssueTypes/Edit/5
        [Authorize(Roles = "Create_Edit_Del_IssueType")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType.FindAsync(id);
            if (issueType == null)
            {
                return View("IssueTypeDeleted");
            }
            return View(issueType);
        }

        // POST: IssueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create_Edit_Del_IssueType")]
        public async Task<IActionResult> Edit(int id, [Bind("IssueTypeId,Name,IssueDescription")] IssueType issueType)
        {
            if (id != issueType.IssueTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueTypeExists(issueType.IssueTypeId))
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
            return View(issueType);
        }

        // GET: IssueTypes/Delete/5
        [Authorize(Roles = "Create_Edit_Del_IssueType")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssueType == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueType
                .FirstOrDefaultAsync(m => m.IssueTypeId == id);
            if (issueType == null)
            {
                return View("IssueTypeDeleted");
            }
            
            return View(issueType);
        }

        // POST: IssueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Create_Edit_Del_IssueType")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueType == null)
            {
                return Problem("Entity set 'SupermarketDbContext.IssueType'  is null.");
            }
            var issueType = await _context.IssueType.FindAsync(id);
            if (issueType != null)
            {
                try
                {
                    _context.IssueType.Remove(issueType);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    ViewBag.ErrorDeleting = "The Issue Type cannot be deleted because it is associated with an issue.";
                    return View();
                }
            }

            await _context.SaveChangesAsync();
            return View("DeleteCompleted", issueType);
        }

        private bool IssueTypeExists(int id)
        {
            return (_context.IssueType?.Any(e => e.IssueTypeId == id)).GetValueOrDefault();
        }
    }
}

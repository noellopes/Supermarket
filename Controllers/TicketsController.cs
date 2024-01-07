using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    public class TicketsController : Controller
    {
        
        private readonly SupermarketDbContext _context;

        public TicketsController(SupermarketDbContext context)
        {
            _context = context;
        }



        // GET: Tickets
        public async Task<IActionResult> Index (int page = 1,int departmentName = 0)
        {
   

        ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

        var tickets = from b in _context.Tickets.Include(b => b.Departments) select b;
      
    
        if (departmentName != 0)
           {
                tickets = tickets.Where(x => x.IDDepartments == departmentName);
            }

        var Departments = await _context.Departments.ToListAsync();


        PagingInfo paging = new PagingInfo
        {
            CurrentPage = page,
            TotalItems = await tickets.CountAsync(),
        };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var tm = new TicketViewModel
            {
                Tickets = await tickets
                .OrderBy(b => b.TicketId)
                .Skip((paging.CurrentPage - 1) * paging.PageSize)
                   .Take(paging.PageSize)
                   .ToListAsync(),
                Departments = Departments,
                PagingInfo = paging,
                SearchDepartment = departmentName,
                //SearchButtonDepartment = departmentButtonName,
            };
                
            return View(tm);
        }

        private int GetDepartmentId(string departmentName)
        {
            return _context.Departments
                .Where(a => a.NameDepartments == departmentName)
                .Select(a => a.IDDepartments)
                .FirstOrDefault();
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {

            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket tickets)
        {
           
          

            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Ticket created sucessfully.";
                return View("Details",tickets);
            }
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["IDDepartments"] = new SelectList(_context.Set<Department>(), "IDDepartments", "NameDepartments");

            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Ticket tickets)
        {
            if (id != tickets.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.TicketId))
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
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'SupermarketDbContext.Tickets'  is null.");
            }
            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets != null)
            {
                _context.Tickets.Remove(tickets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
          return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}

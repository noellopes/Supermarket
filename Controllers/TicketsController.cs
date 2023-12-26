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
    public class TicketsController : Controller
    {
        private static int ticketCount = 0;
        private readonly SupermarketDbContext _context;

        public TicketsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index (string departmentName = "")
        {
            //var ticketList = _context.Tickets.Join(_context.Departments,
            //    ticket => ticket.IDDepartments,
            //    department => department.IDDepartments,
            //    (ticket, department) => new { TicketId = ticket.IDDepartments,
            //        DepartmentName = department.NameDepartments }).ToList();

            //return View(ticketList);
            return _context.Tickets != null ?
                          View(await _context.Tickets.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.Tickets'  is null.");


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

            ViewData["IDDepartments"] = new SelectList(_context.Set<Departments>(), "IDDepartments", "NameDepartments");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Tickets tickets)
        {
            // Increment the ticket count
            ticketCount++;
            // Pass the ticket count to the view
            ViewBag.TicketCount = ticketCount;
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
            ViewData["IDDepartments"] = new SelectList(_context.Set<Departments>(), "IDDepartments", "NameDepartments");

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
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,DataEmissao,DataAtendimento,NumeroDaSenha,Estado,Prioritario,IDDepartments")] Tickets tickets)
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

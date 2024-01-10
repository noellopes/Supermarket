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
    public class ReserveDepartment1Controller : Controller
    {
        private readonly SupermarketDbContext _context;

        public ReserveDepartment1Controller(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: ReserveDepartment1
        public async Task<IActionResult> Index(int page = 1, int reserveid = 0, string employee = "", int numeroDeFunc = 0)
        {
            var reserveDepartments = from r in _context.ReserveDepartment.Include(r => r.Employee) select r;

            if (reserveid != 0)
            {
                reserveDepartments = reserveDepartments.Where(x => x.ReserveId == reserveid);
            }

            if (employee != "")
            {
                reserveDepartments = reserveDepartments.Where(r => r.Employee!.Employee_Name.Contains(employee));
            }

            if (numeroDeFunc != 0)
            {
                reserveDepartments = reserveDepartments.Where(b => b.NumeroDeFunc == numeroDeFunc);
            }

            PagingInfo paging = new PagingInfo
            {
                CurrentPage = page,
                TotalItems = await reserveDepartments.CountAsync(),
            };

            if (paging.CurrentPage <= 1)
            {
                paging.CurrentPage = 1;
            }
            else if (paging.CurrentPage > paging.TotalPages)
            {
                paging.CurrentPage = paging.TotalPages;
            }

            var vm = new ReserveDepartmentViewModel
            {
                ReserveDepartment = await reserveDepartments
                    .OrderBy(r => r.ReserveId)
                    .Skip((paging.CurrentPage - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync(),
                PagingInfo = paging,
                SearchEmployeeName = employee,
                
            };

            return View(vm);
        }

        // GET: ReserveDepartment1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment1 = await _context.ReserveDepartment
                .Include(r => r.Employee)
                .Include(r => r.Reserve)
                .Include(r => r.Ticket)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (reserveDepartment1 == null)
            {
                return NotFound();
            }

            return View(reserveDepartment1);
        }

        // GET: ReserveDepartment1/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name");
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId");
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketId", "TicketId");
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View();
        }

        // POST: ReserveDepartment1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveId,EmployeeId,NumeroDeFunc,TicketID")] ReserveDepartment1 reserveDepartment1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserveDepartment1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment1.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment1.ReserveId);
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketId", "TicketId", reserveDepartment1.TicketID);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View(reserveDepartment1);
        }

        // GET: ReserveDepartment1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment1 = await _context.ReserveDepartment.FindAsync(id);
            if (reserveDepartment1 == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment1.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment1.ReserveId);
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketId", "TicketId", reserveDepartment1.TicketID);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View(reserveDepartment1);
        }

        // POST: ReserveDepartment1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveId,EmployeeId,NumeroDeFunc,TicketID")] ReserveDepartment1 reserveDepartment1)
        {
            if (id != reserveDepartment1.ReserveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserveDepartment1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveDepartment1Exists(reserveDepartment1.ReserveId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Employee_Name", reserveDepartment1.EmployeeId);
            ViewData["ReserveId"] = new SelectList(_context.Reserve, "ReserveId", "ReserveId", reserveDepartment1.ReserveId);
            ViewData["TicketID"] = new SelectList(_context.Ticket, "TicketId", "TicketId", reserveDepartment1.TicketID);
            ViewData["NumeroDeFunc"] = new SelectList(_context.Reserve, "NumeroDeFunc", "NumeroDeFunc");
            return View(reserveDepartment1);
        }

        // GET: ReserveDepartment1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReserveDepartment == null)
            {
                return NotFound();
            }

            var reserveDepartment1 = await _context.ReserveDepartment
                .Include(r => r.Employee)
                .Include(r => r.Reserve)
                .Include(r => r.Ticket)
                .FirstOrDefaultAsync(m => m.ReserveId == id);
            if (reserveDepartment1 == null)
            {
                return NotFound();
            }

            return View(reserveDepartment1);
        }

        // POST: ReserveDepartment1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReserveDepartment == null)
            {
                return Problem("Entity set 'SupermarketDbContext.ReserveDepartment'  is null.");
            }
            var reserveDepartment1 = await _context.ReserveDepartment.FindAsync(id);
            if (reserveDepartment1 != null)
            {
                _context.ReserveDepartment.Remove(reserveDepartment1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveDepartment1Exists(int id)
        {
          return (_context.ReserveDepartment?.Any(e => e.ReserveId == id)).GetValueOrDefault();
        }
    }
}

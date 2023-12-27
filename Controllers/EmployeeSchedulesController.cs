﻿using System;
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
    public class EmployeeSchedulesController : Controller
    {
        private readonly SupermarketDbContext _context;

        public EmployeeSchedulesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeSchedules
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeSchedule != null ? 
                          View(await _context.EmployeeSchedule.ToListAsync()) :
                          Problem("Entity set 'SupermarketDbContext.EmployeeSchedule'  is null.");
        }

        // GET: EmployeeSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchTime")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,EmployeeId,Date,CheckInTime,CheckOutTime,LunchStartTime,LunchTime")] EmployeeSchedule employeeSchedule)
        {
            if (id != employeeSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeScheduleExists(employeeSchedule.ScheduleId))
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
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeSchedule == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // POST: EmployeeSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeSchedule == null)
            {
                return Problem("Entity set 'SupermarketDbContext.EmployeeSchedule'  is null.");
            }
            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule != null)
            {
                _context.EmployeeSchedule.Remove(employeeSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeScheduleExists(int id)
        {
          return (_context.EmployeeSchedule?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
    }
}